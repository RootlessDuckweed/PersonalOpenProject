using UnityEngine;
using Cainos.LucidEditor;
using Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(DisableInPlayModeAttribute))]
    public class DisableInPlayModeAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            DisableInPlayModeAttribute disableInPlayMode = (DisableInPlayModeAttribute)attribute;
            property.isEditable = !Application.isPlaying;
        }
    }
}