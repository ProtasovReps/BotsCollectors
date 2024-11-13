using System;
using UnityEngine;

public class Resource : MonoBehaviour, IUnitTarget
{
    public event Action PickedUp;

    public Transform Transform => transform;

    public void Collect() => PickedUp?.Invoke();
}