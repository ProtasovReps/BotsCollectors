using System;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [SerializeField] private UnitFactory _factory;
    [SerializeField] private float _startUnitCount;

    private Queue<Unit> _freeUnits;
    private List<Unit> _units;

    public event Action UnitReleased;

    public int FreeUnitsCount => _freeUnits.Count;

    private void OnEnable()
    {
        if (_freeUnits != null)
        {
            foreach (Unit unit in _freeUnits)
            {
                unit.WorkedOut += SaveUnit;
            }
        }
    }

    private void OnDisable()
    {
        foreach (Unit unit in _freeUnits)
        {
            unit.WorkedOut -= SaveUnit;
        }
    }

    public void Initialize()
    {
        _freeUnits = new Queue<Unit>();
        _units = new List<Unit>();

        for (int i = 0; i < _startUnitCount; i++)
            AddUnit();
    }

    public void SaveUnit(Unit unit)
    {
        _freeUnits.Enqueue(unit);
        UnitReleased?.Invoke();
    }

    public Unit GetUnit() => _freeUnits.Dequeue();

    private void AddUnit()
    {
        Unit newUnit = _factory.Produce();

        newUnit.WorkedOut += SaveUnit;
        _units.Add(newUnit);
        _freeUnits.Enqueue(newUnit);
        UnitReleased?.Invoke();
    }
}