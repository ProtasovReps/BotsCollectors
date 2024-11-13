using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _scanRadius;
    [SerializeField, Min(0.5f)] private float _scanDelay;

    private Coroutine _coroutine;

    public event Action<List<Resource>> ResourcesFound;

    public void StartScanDelayed()
    {
        _coroutine = StartCoroutine(ScanDelayed());
    }

    private IEnumerator ScanDelayed()
    {
        var delay = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            Scan();

            yield return delay;
        }
    }

    private void Scan()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius, _layerMask);

        if (colliders.Length > 0)
        {
            var resources = new List<Resource>();

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Resource resource))
                {
                    resources.Add(resource);
                }
            }

            ResourcesFound?.Invoke(resources);
        }
    }
}