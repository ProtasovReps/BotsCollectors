using UnityEngine;

public class FlagFactory : MonoBehaviour
{
    [SerializeField] private Flag _prefab;

    public Flag Produce() => Instantiate(_prefab);
}
