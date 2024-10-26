using UnityEngine;

public class DistanceComparer : MonoBehaviour
{
    [SerializeField] private float _maxSquareDistance;

    public bool CompareDistances(Transform target)
    {
        Vector3 offset = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;

        return offset.sqrMagnitude <= _maxSquareDistance;
    }

    public bool ComparePositions(Transform target)
    {
        return transform.position.x == target.position.x && transform.position.z == target.position.z;
    }
}
