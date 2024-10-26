using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Barrack _barrack;
    [SerializeField] private Scanner _scanner;

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

    private void OnResourcesFound(List<Resource> resources)
    {
        foreach (Resource resource in resources)
            SetUnitTarget(resource);
    }

    private void SetUnitTarget(Resource resource)
    {
        if (_barrack.TryGetUnit(out Unit unit) == false)
            return;

        resource.SetBusyState();
        unit.SetTargetResource(resource);
    }
}