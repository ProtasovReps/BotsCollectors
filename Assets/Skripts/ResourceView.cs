using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private ResourceStash _stash;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _stash.AmountChanged += OnAmountChanged;
    }

    private void OnDisable()
    {
        _stash.AmountChanged -= OnAmountChanged;
    }

    private void OnAmountChanged()
    {
        _text.text = _stash.ResourceCount.ToString();
    }
}
