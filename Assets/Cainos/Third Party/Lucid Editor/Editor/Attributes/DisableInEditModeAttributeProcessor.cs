using UnityEngine;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(DisableInEditModeAttribute))]
    public class DisableInEditModeAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            DisableInEditModeAttribute disableInEditMode = (DisableInEditModeAttribute)attribute;
            property.isEditable = Application.isPlaying;
        }
    }
}