using UnityEditor;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(HelpBoxIfAttribute))]
    public class HelpBoxIfAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            HelpBoxIfAttribute helpBoxIf = (HelpBoxIfAttribute)attribute;
            if (ReflectionUtil.GetValueBool(property.parentObject, helpBoxIf.condition))
            {
                EditorGUILayout.HelpBox(helpBoxIf.message, (MessageType)helpBoxIf.type);
            }
        }
    }
}