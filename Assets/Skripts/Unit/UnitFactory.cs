using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private Unit _prefab;
    [SerializeField] private float _spawnUpPosition;
    [SerializeField] private BaseBuilder _baseBuilder;

    public Unit Produce(ResourceStash resourceStash, Vector3 spawnPosition)
    {
        var newPosition = new Vector3(spawnPosition.x, _spawnUpPosition, spawnPosition.z);
        Unit newUnit = Instantiate(_prefab, newPosition, Quaternion.identity);

        newUnit.SetStash(resourceStash);
        return newUnit;
    }
}
