using System;
using System.Linq;
using AT = System.AttributeTargets;

namespace Greg.Utils.TagSearcher
{
    [AttributeUsage(AT.Class | AT.Delegate | AT.Enum | AT.Interface | AT.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class ScriptTagAttribute : Attribute
    {
        public object[] Tags { get; }

        public ScriptTagAttribute(ArchitectureTag architectureTag, params object[] tags)
        {
            Tags = tags.Prepend(architectureTag).ToArray();
        }
    }

    //

    public enum ArchitectureTag
    {
        Handler,
        Event,
        Data,
        Component,
        NetworkMessage,
        Util,
        HolderResource,
        ServiceResource,
        Global,
    }
}
