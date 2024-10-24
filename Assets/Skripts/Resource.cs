using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Resource : MonoBehaviour 
{
    private void Awake() => GetComponent<Rigidbody>().isKinematic = true;
}