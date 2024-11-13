using System;
using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private BaseFactory _factory;

    public event Action<Base> BaseBuilt;
    public event Action<Flag> FlagWorkedOut;

    public void SetUnitBuilder(Unit unit)
    {
        unit.FlagReached += OnFlagReached;
    }

    private void OnFlagReached(Flag flag, Unit unit)
    {
        Base newBase = _factory.Produce();

        newBase.transform.position = flag.transform.position;
        unit.FlagReached -= OnFlagReached;

        newBase.Barrack.AddNewUnit(unit);
        unit.SetStash(newBase.ResourceStash as ResourceStash);

        BaseBuilt?.Invoke(newBase);
        FlagWorkedOut?.Invoke(flag);
    }
}
