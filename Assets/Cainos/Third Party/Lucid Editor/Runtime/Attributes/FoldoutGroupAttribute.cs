using System;

namespace Cainos.Third_Party.Lucid_Editor.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property)]
    public class FoldoutGroupAttribute : PropertyGroupAttribute
    {
        public FoldoutGroupAttribute(string groupName) : base(groupName) { }
    }
}