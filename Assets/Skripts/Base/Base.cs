using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IFlagSettable
{
    [SerializeField] private Barrack _barrack;
    [SerializeField] private ResourceScanner _scanner;
    [SerializeField] private ResourceStash _resourceStash;
    [SerializeField] private ResourceContolCenter _resourceCenter;
    [SerializeField] private PriceList _prices;

    private Flag _targetFlag;
    private bool _ifNewbaseBought;
    private bool _isFlagged => _targetFlag != null;

    public event Action<Unit> UnitBuilderSent;

    public IResourceStash ResourceStash => _resourceStash;
    public IUnitAddable Barrack => _barrack;

    private void OnEnable()
    {
        _scanner.ResourcesFound += OnResourcesFound;
        _barrack.UnitReleased += OnUnitReleased;
        _resourceStash.AmountChanged += OnResourceAmountChanged;
    }

    private void OnDisable()
    {
        _scanner.ResourcesFound -= OnResourcesFound;
        _barrack.UnitReleased -= OnUnitReleased;
        _resourceStash.AmountChanged -= OnResourceAmountChanged;
    }

    public void Initialize(UnitFactory factory, ResourceDataBase dataBase)
    {
        _resourceCenter.Initialize(_resourceStash, dataBase);
        _barrack.Initialize(factory, _resourceStash);
        _scanner.StartScanDelayed();
    }

    public void SetFlag(Flag flag) => _targetFlag = flag;

    public void RemoveFlag() => _targetFlag = null;

    private void SetUnitTarget()
    {
        if (_barrack.FreeUnitsCount == 0)
            return;

        if (_ifNewbaseBought && _barrack.IsReadyToBuild)
            SetTargetFlag();
        else
            SetTargetResource();
    }

    private void SetTargetResource()
    {
        if (_resourceCenter.TryGetResource(out Resource resource) == false)
            return;

        Unit unit = _barrack.GetUnit();

        _barrack.SetUnitTarget(unit, resource);
    }

    private void SetTargetFlag()
    {
        Unit unit = _barrack.GetUnit();

        _barrack.SetUnitTarget(unit, _targetFlag);
        _barrack.RemoveUnit(unit);
        UnitBuilderSent?.Invoke(unit);

        _ifNewbaseBought = false;
    }

    private void OnResourceAmountChanged()
    {
        int targetPrice;
        bool isBaseTarget = _isFlagged && _barrack.IsReadyToBuild;

        if (isBaseTarget)
            targetPrice = _prices.NewBasePrice;
        else
            targetPrice = _prices.NewUnitPrice;

        if (_resourceStash.ResourceCount < targetPrice)
            return;

        _resourceStash.SpendResources(targetPrice);

        if (isBaseTarget)
            _ifNewbaseBought = true;
        else
            _barrack.CreateUnit();
    }

    private void OnResourcesFound(List<Resource> resources)
    {
        _resourceCenter.AddResources(resources);
        SetUnitTarget();
    }

    private void OnUnitReleased() => SetUnitTarget();
}