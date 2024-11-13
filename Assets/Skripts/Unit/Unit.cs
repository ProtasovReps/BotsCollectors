using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitCollector _collector;
    
    private ResourceStash _stash;
    private Coroutine _coroutine;

    public event Action<Unit> WorkedOut;
    public event Action<Flag, Unit> FlagReached;

    private void OnEnable()
    {
        _mover.Reached += OnReached;
    }

    private void OnDisable()
    {
        _mover.Reached -= OnReached;
    }

    public void SetStash(ResourceStash resourceStash)
    {
        _stash = resourceStash;
    }

    public void SetTarget(IUnitTarget target)
    {
        _mover.Move(target);
    }

    private void OnReached(IUnitTarget target)
    {
        if(target is Resource)
        {
            _collector.TakeResource(target as Resource);
            _mover.Move(_stash);
        }

        if(target is Flag)
        {
            FlagReached?.Invoke(target as Flag, this);
        }

        if(target is ResourceStash)
        {
            _collector.StashResource(_stash);
            WorkedOut?.Invoke(this);
        }
    }
}
