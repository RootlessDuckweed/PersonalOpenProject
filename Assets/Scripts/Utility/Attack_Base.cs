using Character;
using UnityEngine;
using Utility.EnumType;


namespace Utility
{
    public class Attack_Base : MonoBehaviour
    {
        public AilmentType AdditionalAilment = AilmentType.None;
        public float originalWeaponDamage; //角色配带的武器伤害加成
        protected CharacterStats stat; //角色的自身属性加成
        [SerializeField] protected DamageType damageType = DamageType.Physical;
        protected virtual void  OnTriggerEnter2D(Collider2D other)
        {
            // take damage
            if(stat==null) return;
            Enemy.Enemy enemy= other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(stat.gameObject,originalWeaponDamage,enemy.FrozenTimeBeAttacked,stat,damageType,AdditionalAilment);
                return;
            }

            var entity = other.GetComponent<Entity>();
            entity?.TakeDamage(stat.gameObject,originalWeaponDamage,false,stat,damageType,AdditionalAilment);
        }

       
        
    }
}