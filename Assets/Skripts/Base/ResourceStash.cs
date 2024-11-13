using System;
using UnityEngine;

public class ResourceStash : MonoBehaviour, IResourceStash, IUnitTarget
{
    public event Action<Resource> Stashed;
    public event Action AmountChanged;

    public int ResourceCount { get; private set; }

    public Transform Transform => transform;

    public void AddResource(Resource resource)
    {
        resource.transform.parent = null;
        ResourceCount++;
        Stashed?.Invoke(resource);
        AmountChanged?.Invoke();
    }

    public void SpendResources(int  resourceSpendAmount)
    {
        if (ResourceCount < resourceSpendAmount || resourceSpendAmount < 0)
            throw new ArgumentOutOfRangeException();

        ResourceCount -= resourceSpendAmount;
        AmountChanged?.Invoke();
    }
}
