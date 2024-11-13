using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private IResourceStash _stash;

    private void OnEnable()
    {
        if (_stash != null)
            _stash.AmountChanged += OnResourceAmountChanged;
    }

    private void OnDisable()
    {
        _stash.AmountChanged += OnResourceAmountChanged;
    }

    public void Initialize(IResourceStash stash)
    {
        _stash = stash;
        _stash.AmountChanged += OnResourceAmountChanged;
    }

    private void OnResourceAmountChanged()
    {
        _text.text = _stash.ResourceCount.ToString();
    }
}
