using System;
using UnityEngine;

public class ResourceStash : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;

    public int ResourceCount { get; private set; }

    public event Action AmountChanged;

    public void AddResource(Resource resource)
    {
        resource.transform.parent = null;
        _pool.Release(resource);
        ResourceCount++;
        AmountChanged?.Invoke();
    }
}
