using System.Linq;
using UnityEngine;

public class SpawnPointStash : MonoBehaviour 
{
    [SerializeField] private SpawnPoint[] _spawnPoints;

    public bool TryGetRandomSpawnPoint(out SpawnPoint spawnPoint)
    {
        SpawnPoint[] freesSpawnPoints= _spawnPoints.Where(spawnPoint => spawnPoint.IsFree).ToArray();

        if (freesSpawnPoints.Length == 0)
        {
            spawnPoint = null;
            return false;
        }

        int randomIndex = Random.Range(0, freesSpawnPoints.Length);

        spawnPoint = freesSpawnPoints[randomIndex];
        return true;
    }
}
