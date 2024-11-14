using System.Collections.Generic;
using UnityEngine;

public class BuildControlCenter : MonoBehaviour
{
    [SerializeField] private BaseBuilder _builder;

    private List<Base> _bases;

    private void OnEnable()
    {
        _builder.BaseBuilt += OnBaseBuilt;

        if(_bases != null)
        {
            foreach (Base currentBase in _bases)
            {
                currentBase.UnitBuilderSent += OnUnitBuilderSent;
            }
        }
    }

    private void OnDisable()
    {
        _builder.BaseBuilt -= OnBaseBuilt;

        if (_bases != null)
        {
            foreach (Base currentBase in _bases)
            {
                currentBase.UnitBuilderSent -= OnUnitBuilderSent;
            }
        }
    }

    public void Initialize(Base startBase)
    {
        _bases = new List<Base>();

        OnBaseBuilt(startBase);
    }

    private void OnUnitBuilderSent(Unit unit)
    {
        _builder.SetUnitBuilder(unit);
    }

    private void OnBaseBuilt(Base newBase)
    {
        _bases.Add(newBase);

        newBase.UnitBuilderSent += OnUnitBuilderSent;
    }
}
