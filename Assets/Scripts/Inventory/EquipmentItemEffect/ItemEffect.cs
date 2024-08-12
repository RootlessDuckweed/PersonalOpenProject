using UnityEngine;

// 物品属性特效加成 最好是交给类处理 而不是SO 每个物品特效属性加成装备在不同的角色上都应该具有独立性，而SO是全局影响
namespace Inventory
{
    public class ItemEffect : ScriptableObject
    {
        /// <summary>
        /// 正常的装备效果 如增加血量 设置属性等 一些可以在装备的那一刻获得的永久效果
        /// </summary>
        public virtual void ExecuteNormalEffect(GameObject player=null,GameObject enemy=null)
        {
            
        }

        public virtual void CancelNormalEffect(GameObject player=null,GameObject enemy=null)
        {
            
        }

        /// <summary>
        /// 特别的效果，在具体的实例中执行，比如需要精确的点位触发，无法在装备的那一刻 进行执行 需要在特定的场景去执行
        /// </summary>
        /// <param name="player">特效使用者</param>
        /// <param name="enemy">打击目标</param>
        public virtual void ExecuteSpecialEffect(GameObject player=null,GameObject enemy=null)
        {
            
        }

        public virtual void CancelSpecialEffect(GameObject player=null,GameObject enemy=null)
        {
            
        }
    }
}