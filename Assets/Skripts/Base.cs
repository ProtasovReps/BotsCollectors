using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Barrack _barrack;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ResourceStash _resourceStash;
    [SerializeField] private ResourceContolCenter _resourceCenter;

    private void OnEnable()
    {
        _scanner.ResourcesFound += OnResourcesFound;
        _barrack.UnitReleased += OnUnitReleased;
    }

    private void OnDisable()
    {
        _scanner.ResourcesFound -= OnResourcesFound;
        _barrack.UnitReleased -= OnUnitReleased;
    }

    public void Initialize()
    {
        _scanner.StartScanDelayed();
        _resourceCenter.Initialize(_resourceStash);
        _barrack.Initialize();
    }

    private void SetUnitTarget()
    {
        if (_barrack.FreeUnitsCount == 0)
            return;

        if (_resourceCenter.TryGetResource(out Resource resource) == false)
            return;

        Unit unit = _barrack.GetUnit();
        unit.SetTargetResource(resource);
    }

    private void OnResourcesFound(List<Resource> resources)
    {
        _resourceCenter.AddResources(resources);
        SetUnitTarget();
    }

    private void OnUnitReleased() => SetUnitTarget();
}