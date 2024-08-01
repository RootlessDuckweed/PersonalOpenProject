using UnityEditor;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            property.isEditable = false;
        }
    }
}