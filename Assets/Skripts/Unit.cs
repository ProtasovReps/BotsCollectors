using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMovement _movement;
    [SerializeField] private UnitCollector _collector;
    [SerializeField] private DistanceComparer _distanceChecker;

    private ResourceStash _stash;
    private Coroutine _coroutine;

    public event Action<Unit> WorkedOut;

    public void Initialize(ResourceStash resourceStash)
    {
        _stash = resourceStash;
    }

    public void SetTargetResource(Resource resource)
    {
        _coroutine = StartCoroutine(BringResource(resource));
    }

    private IEnumerator BringResource(Resource resource)
    {
        while (_distanceChecker.CompareDistances(resource.transform) == false)
        {
            _movement.Move(resource.transform);
            yield return null;
        }

        _collector.TakeResource(resource);

        while (_distanceChecker.ComparePositions(_stash.transform) == false)
        {
            _movement.Move(_stash.transform);
            yield return null;
        }

        _collector.StashResource(_stash, resource);
        WorkedOut?.Invoke(this);
    }
}
