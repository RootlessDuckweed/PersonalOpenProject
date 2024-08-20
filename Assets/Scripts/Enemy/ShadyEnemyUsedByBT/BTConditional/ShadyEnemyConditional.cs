using BehaviorDesigner.Runtime.Tasks;
using Player.Universal;

namespace Enemy.ShadyEnemyUsedByBT.BTConditional
{
    public class ShadyEnemyConditional : Conditional
    {
        protected ShadyEnemyBT shadyEnemyBt;
        protected PlayerController player;
        public override void OnAwake()
        {
            base.OnAwake();
            shadyEnemyBt = GetComponent<ShadyEnemyBT>();
            player = PlayerManager.Instance.Player;
        }
        
    }
}