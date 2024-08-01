using System.Drawing.Printing;
using Player.Universal;
using UnityEngine;
using Utility.EnumType;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item Effect Lightning",menuName = "Inventory/ItemEffect/Lightning")]
    public class LightningEffect : ItemEffect
    {
        private PlayerAttack[] attacks; 
        public override void ExecuteNormalEffect(GameObject player=null,GameObject enemy=null)
        {
           
            attacks = PlayerManager.Instance.Player.GetComponentsInChildren<PlayerAttack>(true);
            foreach (var t in attacks)
            {
                t.AdditionalAilment = AilmentType.Lightning;
            }
            MonoBehaviour.print("ExecuteEffect");
            
        }

        public override void CancelNormalEffect(GameObject player=null,GameObject enemy=null)
        {
           attacks = PlayerManager.Instance.Player.GetComponentsInChildren<PlayerAttack>(true);
           MonoBehaviour.print(attacks==null);
           foreach (var t in attacks)
           {
               t.AdditionalAilment = AilmentType.None;
           }
           MonoBehaviour.print("CancelEffect");
        }
    }
}