using UnityEditor;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(HelpBoxAttribute))]
    public class HelpBoxAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            HelpBoxAttribute helpBox = (HelpBoxAttribute)attribute;
            EditorGUILayout.HelpBox(helpBox.message, (MessageType)helpBox.type);
        }
    }
}