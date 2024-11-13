using System;
using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private DistanceChecker _distanceChecker;

    private Coroutine _coroutine;

    public event Action<IUnitTarget> Reached;

    public void Move(IUnitTarget target)
    {
        _coroutine = StartCoroutine(FollowTarget(target));
    }

    private IEnumerator FollowTarget(IUnitTarget target)
    {
        while(_distanceChecker.IsCloseEnoughDistance(target.Transform) == false)
        {
            var targetPosition = new Vector3(target.Transform.position.x, transform.position.y, target.Transform.position.z);

            transform.forward = targetPosition - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            yield return null;
        }

        Reached?.Invoke(target);
    }
}
