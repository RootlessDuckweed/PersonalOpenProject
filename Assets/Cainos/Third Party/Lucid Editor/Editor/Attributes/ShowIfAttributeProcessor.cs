using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(ShowIfAttribute))]
    public class ShowIfAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            property.isHidden |= !ReflectionUtil.GetValueBool(property.parentObject, showIf.condition);
        }
    }
}