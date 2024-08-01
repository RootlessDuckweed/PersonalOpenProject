using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(OnValueChangedAttribute))]
    public class OnValueChangedAttributeProcessor : PropertyProcessor
    {
        public override void OnAfterDrawProperty()
        {
            if (property.changed)
            {
                OnValueChangedAttribute onValueChanged = (OnValueChangedAttribute)attribute;
                ReflectionUtil.Invoke(property.parentObject, onValueChanged.methodName, property.serializedProperty.GetValue<object>());
            }
        }
    }
}
