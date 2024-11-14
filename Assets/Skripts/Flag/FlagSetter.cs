using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private ClickPointAnalyzer _scanner;
    [SerializeField] private FlagSpawner _spawner;
    [SerializeField] private BaseBuilder _builder;

    private FlagControlCenter _controlCenter;
    private Base _selectedBase;

    private void OnEnable()
    {
        _builder.FlagWorkedOut += OnFlagWorkedOut;
        _scanner.BaseFound += OnBaseFound;
        _scanner.FlagZoneFound += OnPositionGot;
    }

    private void OnDisable()
    {
        _builder.FlagWorkedOut -= OnFlagWorkedOut;
        _scanner.BaseFound -= OnBaseFound;
        _scanner.FlagZoneFound -= OnPositionGot;
    }

    public void Initialize(MouseReader mouseReader)
    {
        _controlCenter = new FlagControlCenter();

        _spawner.Initialize();
        _scanner.Initialize(mouseReader);
    }

    private void OnBaseFound(Base foundBase)
    {
        if (_selectedBase == foundBase)
            return;

        if (_controlCenter.ContainsBase(foundBase) == false)
            _controlCenter.AddFlagSettable(foundBase);

        _selectedBase = foundBase;
    }

    private void OnPositionGot(Vector3 clickPosition)
    {
        if (_selectedBase == null)
            return;

        if (_controlCenter.TryGetFlag(_selectedBase, out Flag flag) == false)
            flag = _spawner.Get();

        _controlCenter.AddFlag(_selectedBase, flag);

        _selectedBase.SetFlag(flag);
        flag.transform.position = clickPosition;
    }

    private void OnFlagWorkedOut(Flag flag)
    {
        _controlCenter.RemoveFlag(flag);
    }
}