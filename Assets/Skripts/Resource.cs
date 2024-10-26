using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Resource : MonoBehaviour 
{
    public bool IsFree { get; private set; }

    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        SetFreeState();
    }

    public void SetBusyState() => IsFree = false;

    public void SetFreeState() => IsFree = true;
}