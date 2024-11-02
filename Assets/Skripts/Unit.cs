using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitCollector _collector;

    private ResourceStash _stash;
    private Coroutine _coroutine;

    public event Action<Unit> WorkedOut;

    private void OnEnable()
    {
        _mover.Reached += OnReached;
    }

    private void OnDisable()
    {
        _mover.Reached -= OnReached;
    }

    public void Initialize(ResourceStash resourceStash)
    {
        _stash = resourceStash;
    }

    public void SetTargetResource(Resource resource)
    {
        _mover.Move(resource);
    }

    private void OnReached(IUnitTarget target)
    {
        if(target is Resource)
        {
            _collector.TakeResource(target as Resource);
            _mover.Move(_stash);
        }

        if(target is ResourceStash)
        {
            _collector.StashResource(_stash);
            WorkedOut?.Invoke(this);
        }
    }
}
