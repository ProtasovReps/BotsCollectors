using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private Base _startBase;
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private BaseControlCenter _controlCenter;

    private void Start()
    {
        _controlCenter.Initialize(_startBase);
        _resourceSpawner.Initialize();
    }
}
