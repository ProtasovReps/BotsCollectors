using System.Collections.Generic;
using UnityEngine;

public class BaseControlCenter : MonoBehaviour
{
    private ResourceContolCenter _resourceCenter;

    public void Initialize(Base startBase)
    {
        _resourceCenter = new ResourceContolCenter();

        AddBase(startBase);
    }

    private void AddBase(Base newBase)
    {
        newBase.Initialize();
        newBase.ResourcesFound += OnResourcesFound;
        _resourceCenter.AddResourceStash(newBase.Stash);
    }

    private void OnResourcesFound(List<Resource> resources, Base currentBase)
    {
        foreach (Resource resource in resources)
        {
            if (_resourceCenter.IsBusyResource(resource) == false)
            {
                if (currentBase.TrySetTarget(resource))
                {
                    _resourceCenter.AddBusyResource(resource);
                }
            }
        }
    }
}
