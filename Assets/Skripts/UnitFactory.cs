using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private Unit _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ResourceStash _resourceStash;

    public Unit Produce()
    {
        Unit newUnit = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);

        newUnit.Initialize(_resourceStash);
        return newUnit;
    }
}
