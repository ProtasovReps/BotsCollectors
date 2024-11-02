using System.Collections.Generic;

public class ResourceContolCenter
{
    private List<IResourceStash> _resourceStashes;
    private List<Resource> _busyResources;

    public ResourceContolCenter()
    {
        _resourceStashes = new List<IResourceStash>();
        _busyResources = new List<Resource>();
    }

    public bool IsBusyResource(Resource resource) => _busyResources.Contains(resource);

    public void AddBusyResource(Resource resource) => _busyResources.Add(resource);

    public void AddResourceStash(IResourceStash stash)
    {
        _resourceStashes.Add(stash);
        stash.Stashed += OnStashResourceAdded;
    }

    private void OnStashResourceAdded(Resource resource) => _busyResources.Remove(resource);
}
