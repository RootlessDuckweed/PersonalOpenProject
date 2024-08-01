using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(HideIfAttribute))]
    public class HideIfAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            HideIfAttribute hideIf = (HideIfAttribute)attribute;
            property.isHidden |= ReflectionUtil.GetValueBool(property.parentObject, hideIf.condition);
        }
    }
}