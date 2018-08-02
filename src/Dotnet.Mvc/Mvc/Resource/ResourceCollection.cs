using Dotnet.Mvc.Resource.Enums;
using System.Collections.Generic;

namespace Dotnet.Mvc.Resource
{
    public class ResourceCollection : List<ResourceEntity>
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public ResourcePosition Position { get; set; }
    }
}
