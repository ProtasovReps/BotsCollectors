using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private ResourceStash _resourceStash;
    [SerializeField] private SpawnPointStash _spawnPointStash;
    [SerializeField] private float _spawnDelay;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _resourceStash.ResourceAdded += OnResourceAdded;   
    }

    private void OnDisable()
    {
        _resourceStash.ResourceAdded -= OnResourceAdded;
    }

    public void Initialize()
    {
        _pool.Initialize();
        _coroutine = StartCoroutine(SpawnDelayed());
    }

    private IEnumerator SpawnDelayed()
    {
        var delay = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            if (_spawnPointStash.TryGetRandomSpawnPoint(out SpawnPoint spawnPoint))
            {
                Resource resource = _pool.Get();

                resource.transform.position = spawnPoint.transform.position;
            }

            yield return delay;
        }
    }

    private void OnResourceAdded(Resource resource)
    {
        _pool.Release(resource);
        resource.SetFreeState();
    }
}
