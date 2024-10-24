using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Barrack _barrack;
    [SerializeField] private ResourcePool _pool;

    private Queue<Resource> _targets;

    private void OnEnable()
    {
        _pool.ResourceGot += OnResourceGot;
        _barrack.UnitGot += OnUnitGot;
    }

    private void OnDisable()
    {
        _pool.ResourceGot -= OnResourceGot;
        _barrack.UnitGot -= OnUnitGot;
    }

    public void Initialize()
    {
        _barrack.Initialize();

        _targets = new Queue<Resource>();
    }

    private void OnResourceGot(Resource resourse)
    {
        if (_barrack.TryGetUnit(out Unit unit))
            unit.SetTargetResource(resourse);
        else
            _targets.Enqueue(resourse);
    }

    private void OnUnitGot()
    {
        if (_targets.Count > 0)
        {
            if(_barrack.TryGetUnit(out Unit unit))
            {
                unit.SetTargetResource(_targets.Dequeue());
            }
        }
    }
}
