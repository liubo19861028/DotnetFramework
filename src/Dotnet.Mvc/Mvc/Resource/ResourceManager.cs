
using Dotnet.Mvc.Resource.Enums;
using System;
using System.Collections.Generic;

namespace Dotnet.Mvc.Resource
{
    public abstract class ResourceManager
    {
        public static Dictionary<string, ResourceCollection> ScriptSource { get; private set; }
        public static Dictionary<string, ResourceCollection> StyleSource { get; private set; }
        static ResourceManager()
        {
            ScriptSource = new Dictionary<string, ResourceCollection>();
            StyleSource = new Dictionary<string, ResourceCollection>();
        }
        protected ResourceHelper Script(string name)
        {
            return new ResourceHelper(name, ResourceType.Script);
        }
        protected ResourceHelper Style(string name)
        {
            return new ResourceHelper(name, ResourceType.Style);
        }

        protected abstract void InitScript(Func<string, ResourceHelper> script);
        protected abstract void InitStyle(Func<string, ResourceHelper> style);

        public virtual void SetupResource()
        {
            InitScript(Script);
            InitStyle(Style);
        }
    }

}
