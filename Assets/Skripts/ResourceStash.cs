using System;
using UnityEngine;

public class ResourceStash : MonoBehaviour
{
    public event Action<Resource> ResourceAdded;

    public int ResourceCount { get; private set; }

    public void AddResource(Resource resource)
    {
        resource.transform.parent = null;
        ResourceCount++;
        ResourceAdded?.Invoke(resource);
    }
}
