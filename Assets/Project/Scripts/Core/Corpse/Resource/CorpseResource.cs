using UnityEngine;

namespace Project.Scripts.Core
{
    public class CorpseResource
    {
	    public bool HasResource = true;
	    public ResourceType ResourceType;
	    public CorpseResourceObjectBase ResourceObject;

	    public CorpseResource(ResourceType resourceType, CorpseResourceObjectBase resourceObject)
	    {
		    ResourceType = resourceType;
		    ResourceObject = resourceObject;
	    }
    }
}