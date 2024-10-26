using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private Base _base;

    private void Start()
    {
        _base.Initialize();
        _resourceSpawner.Initialize();
    }
}
