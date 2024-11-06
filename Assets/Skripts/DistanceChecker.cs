using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private float _maxSquareDistance = 1f;

    public bool IsCloseEnoughDistance(Transform target)
    {
        var offset = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;

        return offset.sqrMagnitude <= _maxSquareDistance;
    }
}
