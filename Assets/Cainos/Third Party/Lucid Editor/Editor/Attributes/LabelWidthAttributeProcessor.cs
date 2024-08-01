using UnityEditor;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(LabelWidthAttribute))]
    public class LabelWidthAttributeProcessor : PropertyProcessor
    {
        private float defaultWidth;

        public override void OnBeforeDrawProperty()
        {
            defaultWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = ((LabelWidthAttribute)attribute).width;
        }

        public override void OnAfterDrawProperty()
        {
            EditorGUIUtility.labelWidth = defaultWidth;
        }
    }
}