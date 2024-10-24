using System;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [SerializeField] private UnitFactory _factory;
    [SerializeField] private float _unitCount;

    private Queue<Unit> _freeUnits;
    private List<Unit> _units;

    public event Action UnitGot;

    public void Initialize()
    {
        _freeUnits = new Queue<Unit>();
        _units = new List<Unit>();

        for (int i = 0; i < _unitCount; i++)
            AddUnit();
    }

    public void SaveUnit(Unit unit)
    {
        _freeUnits.Enqueue(unit);
        UnitGot?.Invoke();
    }

    public bool TryGetUnit(out Unit unit)
    {
        if (_freeUnits.Count <= 0)
        {
            unit = null;
            return false;
        }

        unit = _freeUnits.Dequeue();
        return true;
    }

    private void AddUnit()
    {
        Unit newUnit = _factory.Produce();

        newUnit.WorkedOut += SaveUnit;
        _units.Add(newUnit);
        _freeUnits.Enqueue(newUnit);
    }
}