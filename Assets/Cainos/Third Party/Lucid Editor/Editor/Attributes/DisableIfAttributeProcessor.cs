using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(DisableIfAttribute))]
    public class DisableIfAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            DisableIfAttribute disableIf = (DisableIfAttribute)attribute;
            property.isEditable = !ReflectionUtil.GetValueBool(property.parentObject, disableIf.condition);
        }
    }
}