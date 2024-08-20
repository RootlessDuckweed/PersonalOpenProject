using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ShadyEnemyShoot : ShadyEnemyAction
    {
        public float chanceToCreateShoot;
        public override void OnStart()
        {
            if(chanceToCreateShoot>=Random.Range(0,100))
                shadyEnemyBt.CreateShadyShoot();
        }
    }
}