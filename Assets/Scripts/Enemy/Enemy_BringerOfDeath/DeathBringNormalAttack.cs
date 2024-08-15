using System;
using Character;
using Utility;

namespace Enemy.Enemy_BringerOfDeath
{
    public class DeathBringNormalAttack : Attack_Base
    {
        private void Awake()
        {
            stat = GetComponentInParent<EnemyStats>();
        }
    }
}