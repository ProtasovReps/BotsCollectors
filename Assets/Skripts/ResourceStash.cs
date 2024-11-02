using System;
using UnityEngine;

public class ResourceStash : MonoBehaviour, IResourceStash, IUnitTarget
{
    public event Action<Resource> Stashed;

    public int ResourceCount { get; private set; }

    public Transform Transform => transform;

    public void AddResource(Resource resource)
    {
        resource.transform.parent = null;
        ResourceCount++;
        Stashed?.Invoke(resource);
    }
}
