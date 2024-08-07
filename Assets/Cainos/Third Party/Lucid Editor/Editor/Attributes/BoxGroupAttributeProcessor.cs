using UnityEngine;
using UnityEditor;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomGroupProcessor(typeof(BoxGroupAttribute))]
    public class BoxGroupAttributeProcessor : PropertyGroupProcessor
    {
        public override void BeginPropertyGroup()
        {
            LucidEditorGUILayout.BeginLayoutIndent(EditorGUI.indentLevel);
            LucidEditorGUILayout.BeginBoxGroup(attribute.name, GUILayout.MinWidth(0));
        }

        public override void EndPropertyGroup()
        {
            LucidEditorGUILayout.EndBoxGroup();
            LucidEditorGUILayout.EndLayoutIndent();
            
            EditorGUILayout.Space(2);
        }
    }
}