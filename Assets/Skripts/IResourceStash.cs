using System;

public interface IResourceStash 
{
    public event Action<Resource> Stashed;    
}
