using System.Collections.Generic;
using UnityEngine;

public class ResourceContolCenter : MonoBehaviour
{
    private IResourceStash _stash;
    private ResourceDataBase _data;
    private List<Resource> _busyResources;
    private Queue<Resource> _freeResources;

    private void OnEnable()
    {
        if (_stash != null)
            _stash.Stashed += OnStashResourceAdded;
    }

    private void OnDisable()
    {
        _stash.Stashed -= OnStashResourceAdded;
    }

    public void Initialize(IResourceStash resourceStash, ResourceDataBase dataBase)
    {
        _data = dataBase;
        _freeResources = new Queue<Resource>();
        _busyResources = new List<Resource>();
        _stash = resourceStash;

        _stash.Stashed += OnStashResourceAdded;
    }

    public void AddResources(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            if (_freeResources.Contains(resource) == false && _busyResources.Contains(resource) == false)
            {
                if (_data.IsFoundResource(resource) == false)
                {
                    _freeResources.Enqueue(resource);
                    _data.AddResource(resource);
                }
            }
        }
    }

    public bool TryGetResource(out Resource resource)
    {
        if (_freeResources.Count > 0)
        {
            resource = _freeResources.Dequeue();
            _busyResources.Add(resource);
        }
        else
        {
            resource = null;
        }

        return resource != null;
    }

    private void OnStashResourceAdded(Resource resource)
    {
        _busyResources.Remove(resource);
        _data.RemoveResource(resource);
    }
}
