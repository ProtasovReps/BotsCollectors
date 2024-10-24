using System.Linq;
using UnityEngine;

public class SpawnPointStash : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;

    public bool TryGetRandomSpawnPoint(out SpawnPoint spawnPoint)
    {
        SpawnPoint[] freeSpawnPoints = _spawnPoints.Where(spawnPoint => spawnPoint.IsFree).ToArray();

        if(freeSpawnPoints.Length == 0)
        {
            spawnPoint = null;
            return false;
        }

        int randomIndex = Random.Range(0, freeSpawnPoints.Length);
        
        spawnPoint = freeSpawnPoints[randomIndex];
        return true;
    }
}
