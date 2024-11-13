using UnityEngine;

public class UnitCollector : MonoBehaviour
{
    [SerializeField] private Transform _takePoint;

    private Resource _resource;

    public void TakeResource(Resource resource)
    {
        _resource = resource;
        resource.transform.position = _takePoint.position;
        resource.Collect();
        resource.transform.SetParent(transform);
    }

    public void StashResource(ResourceStash stash)
    {
        if (_resource == null)
            return;

        stash.AddResource(_resource);
        _resource = null;
    }
}
