using UnityEngine;
using UnityEngine.Pool;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;

    private ObjectPool<Resource> _pool;

    public Resource Get() => _pool.Get();

    public void Release(Resource resource) => _pool.Release(resource);

    public void Initialize() => CreatePool();

    private void CreatePool()
    {
        _pool = new ObjectPool<Resource>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (resource) => resource.gameObject.SetActive(true),
            actionOnRelease: (resource) => resource.gameObject.SetActive(false),
            actionOnDestroy: (resource) => Destroy(resource.gameObject));
    }
}
