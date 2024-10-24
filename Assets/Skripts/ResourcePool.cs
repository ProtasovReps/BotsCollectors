using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private SpawnPointStash _stash;
    [SerializeField] private float _delay;

    private ObjectPool<Resource> _pool;
    private Coroutine _coroutine;

    public event Action<Resource> ResourceGot;

    public void Release(Resource resource) => _pool.Release(resource);

    public void Initialize()
    {
        CreatePool();
        _coroutine = StartCoroutine(GetDelayed());
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Resource>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (resource) => resource.gameObject.SetActive(true),
            actionOnRelease: (resource) => resource.gameObject.SetActive(false),
            actionOnDestroy: (resource) => Destroy(resource.gameObject));
    }

    private void Get()
    {
        if (_stash.TryGetRandomSpawnPoint(out SpawnPoint spawnPoint) == false)
            return;

        _pool.Get(out Resource resource);
        resource.transform.position = spawnPoint.transform.position;
        ResourceGot?.Invoke(resource);
    }

    private IEnumerator GetDelayed()
    {
        var delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            Get();
            yield return delay;
        }
    }
}
