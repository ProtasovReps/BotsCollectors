using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _takePoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxResourceDistance;

    private ResourceStash _stash;
    private Coroutine _coroutine;
    private Resource _targetResource;

    public event Action<Unit> WorkedOut;

    public void Initialize(ResourceStash resourceStash)
    {
        _stash = resourceStash;
    }

    public void SetTargetResource(Resource resource)
    {
        _targetResource = resource;
        _coroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return StartCoroutine(FollowTarget(_targetResource.transform));

        TakeResource();

        yield return StartCoroutine(FollowTarget(_stash.transform));

        StashResource();
    }

    private IEnumerator FollowTarget(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

        while (CheckDistance(target.transform))
        {
            transform.forward = targetPosition - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private bool CheckDistance(Transform target)
    {
        Vector3 offset = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;

        return offset.sqrMagnitude >= _maxResourceDistance;
    }

    private void TakeResource()
    {
        _targetResource.transform.position = _takePoint.position;
        _targetResource.transform.SetParent(transform);
    }

    private void StashResource()
    {
        _stash.AddResource(_targetResource);
        _targetResource = null;

        WorkedOut?.Invoke(this);
    }
}
