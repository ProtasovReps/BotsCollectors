using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private BaseFactory _baseFactory;
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private FlagSetter _flagPointSetter;
    [SerializeField] private BuildControlCenter _buildControlCenter;

    private MouseReader _mouseReader;

    private void Start()
    {
        _mouseReader = new MouseReader();

        _baseFactory.Initialize();
        _resourceSpawner.Initialize();
        _flagPointSetter.Initialize(_mouseReader);

        Base startBase = _baseFactory.Produce();
        _buildControlCenter.Initialize(startBase);
    }
}
