using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private float _maxSquareDistance;

    public bool IsValidDistance(IUnitTarget target)
    {
        if(target is Resource)
            return IsMaxDistance(target.Transform);
        else
            return IsEqualPosition(target.Transform);
    }

    private bool IsMaxDistance(Transform target)
    {
        Vector3 offset = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;

        return offset.sqrMagnitude <= _maxSquareDistance;
    }

    private bool IsEqualPosition(Transform target)
    {
        return transform.position.x == target.position.x && transform.position.z == target.position.z;
    }
}
