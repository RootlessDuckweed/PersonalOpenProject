using System;
using Character;
using UnityEngine;
using Utility;

namespace Enemy.Enemy_Shady
{
    public class ShadyShootAttack : Attack_Base
    {

        public void SetupShadyShoot(CharacterStats stats)
        {
            this.stat = stats;
        }
    }
}