using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private Base _startBase;
    [SerializeField] private ResourceSpawner _resourceSpawner;

    private void Start()
    {
        _startBase.Initialize();
        _resourceSpawner.Initialize();
    }
}
