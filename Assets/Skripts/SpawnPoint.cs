using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnPoint : MonoBehaviour
{
    public bool IsFree { get; private set; }

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        IsFree = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resource>(out _))
            IsFree = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Resource>(out _))
            IsFree = true;
    }
}
