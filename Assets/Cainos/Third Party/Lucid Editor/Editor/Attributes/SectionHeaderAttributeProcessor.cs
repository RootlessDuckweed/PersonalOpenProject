using UnityEditor;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(SectionHeaderAttribute))]
    public class SectionHeaderAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            EditorGUILayout.Space(7);
            LucidEditorGUILayout.SectionHeader(((SectionHeaderAttribute)attribute).title);
        }
    }
}