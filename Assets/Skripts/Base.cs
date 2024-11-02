using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Barrack _barrack;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ResourceStash _stash;

    public event Action<List<Resource>, Base> ResourcesFound;

    public IResourceStash Stash => _stash;

    private void OnEnable()
    {
        _scanner.ResourcesFound += OnResourcesFound;
    }

    private void OnDisable()
    {
        _scanner.ResourcesFound -= OnResourcesFound;
    }

    public void Initialize()
    {
        _barrack.Initialize();
        _scanner.StartScanDelayed();
    }

    public bool TrySetTarget(Resource resource)
    {
        if (_barrack.TryGetUnit(out Unit unit) == false)
            return false;

        unit.SetTargetResource(resource);
        return true;
    }

    private void OnResourcesFound(List<Resource> resources)
    {
        ResourcesFound?.Invoke(resources, this);
    }
}