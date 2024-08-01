using System;

namespace Cainos.Third_Party.Lucid_Editor.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LabelWidthAttribute : Attribute
    {
        public readonly float width;

        public LabelWidthAttribute(float width)
        {
            this.width = width;
        }
    }
}