using System.Collections.Generic;

public class FlagControlCenter
{
    private Dictionary<IFlagSettable, Flag> _bases;

    public FlagControlCenter()
    {
        _bases = new Dictionary<IFlagSettable, Flag>();
    }

    public void AddFlagSettable(IFlagSettable flagSettable)
    {
        _bases.Add(flagSettable, null);
    }

    public void AddFlag(IFlagSettable flagSettable, Flag newFlag)
    {
        _bases[flagSettable] = newFlag;
        flagSettable.SetFlag(newFlag);
    }

    public void RemoveFlag(Flag flag)
    {
        foreach (IFlagSettable flagSettable in _bases.Keys)
        {
            if (_bases[flagSettable] == flag)
            {
                _bases[flagSettable] = null;

                flag.SetWorkedOut();
                flagSettable.RemoveFlag();
                return;
            }
        }
    }

    public bool ContainsBase(IFlagSettable flagSettable)
    {
        return _bases.ContainsKey(flagSettable);
    }

    public bool TryGetFlag(IFlagSettable flagSettable, out Flag flag)
    {
        if (_bases.ContainsKey(flagSettable))
            flag = _bases[flagSettable];
        else 
            flag = null;

        return flag != null;
    }
}
