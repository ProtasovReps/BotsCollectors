using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private ObjectPool<Resource> _pool;
    [SerializeField] private ResourceStash _resourceStash;
    [SerializeField] private SpawnPointStash _spawnPointStash;
    [SerializeField] private float _spawnDelay;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _resourceStash.Stashed += OnResourceAdded;   
    }

    private void OnDisable()
    {
        _resourceStash.Stashed -= OnResourceAdded;
    }

    public void Initialize()
    {
        CreatePool();
        _coroutine = StartCoroutine(SpawnDelayed());
    }

    private void OnResourceAdded(Resource resource)
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