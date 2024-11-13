using System;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour, IUnitAddable
{
    [SerializeField] private int _minUnitCount = 1;
    [SerializeField, Min(1)] private int _newUnitPrice;

    private ResourceStash _stash;
    private UnitFactory _factory;
    private Queue<Unit> _freeUnits;
    private List<Unit> _units;

    public event Action UnitReleased;

    public int UnitsCount => _units.Count;
    public int FreeUnitsCount => _freeUnits.Count;
    public bool IsReadyToBuild => UnitsCount > _minUnitCount;

    private void OnEnable()
    {
        if (_units != null)
        {
            foreach (Unit unit in _freeUnits)
            {
                unit.WorkedOut += SaveUnit;
            }
        }
    }

    private void OnDisable()
    {
        foreach (Unit unit in _units)
        {
            unit.WorkedOut -= SaveUnit;
        }
    }

    public void Initialize(UnitFactory factory, ResourceStash stash)
    {
        _factory = factory;
        _stash = stash;

        _freeUnits = new Queue<Unit>();
        _units = new List<Unit>();

        if (UnitsCount < _minUnitCount)
            CreateUnit();
    }

    public void SaveUnit(Unit unit)
    {
        _freeUnits.Enqueue(unit);
        UnitReleased?.Invoke();
    }

    public void CreateUnit()
    {
        Unit newUnit = _factory.Produce(_stash, transform.position);

        newUnit.transform.SetParent(transform);
        AddNewUnit(newUnit);
    }

    public void AddNewUnit(Unit newUnit)
    {
        newUnit.WorkedOut += SaveUnit;
        _units.Add(newUnit);
        _freeUnits.Enqueue(newUnit);
        UnitReleased?.Invoke();
    }

    public void SetUnitTarget(Unit unit, IUnitTarget unitTarget)
    {
        unit.SetTarget(unitTarget);
    }

    public Unit GetUnit() => _freeUnits.Dequeue();

    public void RemoveUnit(Unit unit)
    {
        unit.WorkedOut -= SaveUnit;

        _units.Remove(unit);
    }
}