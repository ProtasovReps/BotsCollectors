using System.Collections.Generic;

public class ResourceDataBase 
{
    private List<Resource> _foundResources;

    public ResourceDataBase()
    {
        _foundResources = new List<Resource>();
    }

    public bool IsFoundResource(Resource resource) => _foundResources.Contains(resource);

    public void AddResource(Resource resource) => _foundResources.Add(resource);

    public void RemoveResource(Resource resource) => _foundResources.Remove(resource);
}
