using System;

public interface IResourceStash 
{
    public event Action<Resource> Stashed;
    public event Action AmountChanged;

    public int ResourceCount { get; }
}
