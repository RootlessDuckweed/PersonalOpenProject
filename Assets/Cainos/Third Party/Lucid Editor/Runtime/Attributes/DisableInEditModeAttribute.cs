using System;

namespace Cainos.Third_Party.Lucid_Editor.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class DisableInEditModeAttribute : Attribute { }
}
