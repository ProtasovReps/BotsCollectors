using System;
using UnityEngine;

public class Flag : MonoBehaviour, IUnitTarget
{
    public event Action<Flag> WorkedOut;
    
    public Transform Transform => transform;

    public void SetWorkedOut() => WorkedOut?.Invoke(this);
}
