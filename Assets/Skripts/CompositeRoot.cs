using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private Base _base;

    private void Start()
    {
        _base.Initialize();
        _pool.Initialize();
    }
}
