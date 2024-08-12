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
        protected bool isCritical;
        protected virtual void  OnTriggerEnter2D(Collider2D other)
        {
            // take damage
            if(stat==null) return;
            Enemy.Enemy enemy= other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(gameObject,originalWeaponDamage,enemy.isFrozenTime,stat,damageType,AdditionalAilment,out isCritical);
                return;
            }

            var entity = other.GetComponent<Entity>();
            entity?.TakeDamage(gameObject,originalWeaponDamage,false,stat,damageType,AdditionalAilment,out isCritical);
        }

       
        
    }
}