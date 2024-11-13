using System;
using UnityEngine;

public class FlagPointScanner : MonoBehaviour
{
    [SerializeField] private LayerMask _layersToSearch;
    [SerializeField] private float _maxDistance;

    private MouseReader _mouseReader;

    public event Action<Base> BaseFound;
    public event Action<Vector3> FlagZoneFound;

    private void OnEnable()
    {
        if (_mouseReader != null)
        {
            _mouseReader.Enable();
            _mouseReader.Clicked += OnClick;
        }
    }

    private void OnDisable()
    {
        if (_mouseReader != null)
        {
            _mouseReader.Disable();
            _mouseReader.Clicked -= OnClick;
        }
    }

    public void Initialize(MouseReader mouseReader)
    {
        _mouseReader = mouseReader;

        _mouseReader.Enable();
        _mouseReader.Clicked += OnClick;
    }

    private void OnClick(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxDistance, _layersToSearch) == false)
            return;

        if (hitInfo.collider.TryGetComponent(out Base foundBase))
            BaseFound?.Invoke(foundBase);

        if (hitInfo.collider.TryGetComponent(out BuildZone buildZone))
            FlagZoneFound?.Invoke(hitInfo.point);
    }
}
