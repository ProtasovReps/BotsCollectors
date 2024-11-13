using System.Collections.Generic;
using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private FlagFactory _factory;

    private Queue<Flag> _freeFlags;
    private List<Flag> _flags;

    private void OnEnable()
    {
        if (_freeFlags != null)
        {
            foreach (Flag flag in _freeFlags)
            {
                flag.WorkedOut += OnWorkedOut;
            }
        }
    }

    private void OnDisable()
    {
        if (_freeFlags != null)
        {
            foreach (Flag flag in _freeFlags)
            {
                flag.WorkedOut -= OnWorkedOut;
            }
        }
    }

    public void Initialize()
    {
        _freeFlags = new Queue<Flag>();
        _flags = new List<Flag>();
    }

    public Flag Get()
    {
        if (_freeFlags.Count == 0)
            CreateFlag();

        Flag flag = _freeFlags.Dequeue();
        flag.gameObject.SetActive(true);

        return flag;
    }

    private void CreateFlag()
    {
        Flag newFlag = _factory.Produce();

        newFlag.WorkedOut += OnWorkedOut;
        _flags.Add(newFlag);
        _freeFlags.Enqueue(newFlag);
    }

    private void OnWorkedOut(Flag flag)
    {
        _freeFlags.Enqueue(flag);
        flag.gameObject.SetActive(false);
    }
}
