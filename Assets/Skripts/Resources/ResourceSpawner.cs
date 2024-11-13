using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private ObjectPool<Resource> _pool;
    [SerializeField] private SpawnPointStash _spawnPointStash;
    [SerializeField] private float _spawnDelay;

    private List<IResourceStash> _stashes;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        if (_stashes != null)
        {
            foreach (IResourceStash stash in _stashes)
            {
                stash.Stashed += OnResourceStashed;
            }
        }
    }

    private void OnDisable()
    {
        if (_stashes != null)
        {
            foreach (IResourceStash stash in _stashes)
            {
                stash.Stashed -= OnResourceStashed;
            }
        }
    }

    public void Initialize()
    {
        CreatePool();

        _stashes = new List<IResourceStash>();
        _coroutine = StartCoroutine(SpawnDelayed());
    }

    public void AddStash(IResourceStash stash)
    {
        _stashes.Add(stash);

        stash.Stashed += OnResourceStashed;
    }

    private void OnResourceStashed(Resource resource)
    {
        _pool.Release(resource);
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Resource>(
           createFunc: () => Instantiate(_prefab),
           actionOnGet: (resource) => resource.gameObject.SetActive(true),
           actionOnRelease: (resource) => resource.gameObject.SetActive(false),
           actionOnDestroy: (resource) => Destroy(resource.gameObject));
    }

    private IEnumerator SpawnDelayed()
    {
        var delay = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            if (_spawnPointStash.TryGetRandomSpawnPoint(out SpawnPoint spawnPoint))
            {
                Resource resource = _pool.Get();

                spawnPoint.AddResource(resource);
                resource.transform.position = spawnPoint.transform.position;
            }

            yield return delay;
        }
    }
}