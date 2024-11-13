using System;
using UnityEngine;

public class BaseFactory : MonoBehaviour
{
    [SerializeField] private Base _prefab;
    [SerializeField] private ResourceView _viewPrefab;
    [SerializeField] private ResourceSpawner _spawner;
    [SerializeField] private UnitFactory _unitFactory;
    [SerializeField] private float _viewUpPosition;

    private ResourceDataBase _dataBase;

    public void Initialize()
    {
        _dataBase = new ResourceDataBase();
    }

    public Base Produce()
    {
        Base newBase = Instantiate(_prefab);
        ResourceView view = Instantiate(_viewPrefab);
        var viewPosition = new Vector3(newBase.transform.position.x, _viewUpPosition, newBase.transform.position.z);

        view.transform.position = viewPosition;
        view.transform.SetParent(newBase.transform);
        
        newBase.Initialize(_unitFactory, _dataBase);
        _spawner.AddStash(newBase.ResourceStash);
        view.Initialize(newBase.ResourceStash);

        return newBase;
    }
}
