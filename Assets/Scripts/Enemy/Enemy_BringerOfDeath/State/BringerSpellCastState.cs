using Enemy.Enemy_BringerOfDeath.SuperState;
using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerSpellCastState : BringerState
    {
        private float spellTimer;
        private int currentSpells;
        public BringerSpellCastState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            currentSpells = 0;
        }

        public override void Update()
        {
            base.Update();
            spellTimer -= Time.deltaTime;
            if (CanCast())
            {
               var spell= bringerEnemy.CastSpell();
               if (currentSpells == bringerEnemy.amountsOfSpells)
               {
                   spell.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
               }
               bringerEnemy.fx.ScreenShake();
            }
            else if(currentSpells>=bringerEnemy.amountsOfSpells)
            {
                stateMachine.ChangeState(bringerEnemy.teleportState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            bringerEnemy.lastCastTime = Time.time;
        }

        private bool CanCast()
        {
            if (bringerEnemy.amountsOfSpells > currentSpells && spellTimer < 0)
            {
                spellTimer = bringerEnemy.eachSpellCooldown;
                currentSpells++;
                return true;
            }
            return false;
        }
    }
}