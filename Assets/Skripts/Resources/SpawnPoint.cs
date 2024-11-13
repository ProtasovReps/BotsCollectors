using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Resource _resource;

    public bool IsFree => _resource == null;

    public void AddResource(Resource resource)
    {
        _resource = resource;
        _resource.PickedUp += OnPickedUp;
    }

    private void OnPickedUp()
    {
        _resource.PickedUp -= OnPickedUp;
        _resource = null;
    }
}
