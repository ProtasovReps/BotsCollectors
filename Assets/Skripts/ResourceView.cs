using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private ResourceStash _stash;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _stash.Stashed += resource => OnResourceAdded();
    }

    private void OnDisable()
    {
        _stash.Stashed -= resource => OnResourceAdded();
    }

    private void OnResourceAdded()
    {
        _text.text = _stash.ResourceCount.ToString();
    }
}
