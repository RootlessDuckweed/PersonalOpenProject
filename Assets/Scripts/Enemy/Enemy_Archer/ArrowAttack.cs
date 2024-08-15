using System;
using Character;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class ArrowAttack : Attack_Base
    {
        private string targetName = "Player";
        private CapsuleCollider2D cd;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if(targetName == "Player" && LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
                return;
            base.OnTriggerEnter2D(other);
        }

        public void SetupArrow(CharacterStats _stat,CapsuleCollider2D cd)
        {
            stat = _stat;
            this.cd = cd;
        }

        public void ChangeTarget()
        {
            targetName = "Enemy";
        }
    }
}