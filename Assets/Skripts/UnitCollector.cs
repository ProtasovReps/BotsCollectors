using UnityEngine;

public class UnitCollector : MonoBehaviour
{
    [SerializeField] private Transform _takePoint;
    
    public void TakeResource(Resource resource)
    {
        resource.transform.position = _takePoint.position;
        resource.transform.SetParent(transform);
    }

    public void StashResource(ResourceStash stash, Resource resource)
    {
        stash.AddResource(resource);
    }
}
