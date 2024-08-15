using System;
using Character;
using Utility;

namespace Enemy.Enemy_Shady
{
    public class ShadyAttack : Attack_Base
    {
        private void Awake()
        {
            stat = GetComponentInParent<CharacterStats>();
        }
    }
}