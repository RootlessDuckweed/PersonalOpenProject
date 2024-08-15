using Enemy;
using Player.Skill;
using Player.Universal;
using UnityEngine;


namespace Player.State
{
    public class PlayerCounterState : PlayerState
    {
        private bool isSucceed;
        public PlayerCounterState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.SetBool("SuccessfulCounterAttack",false);
        }

        public override void Update()
        {
            base.Update();
            Collider2D[] colliders = player.GetCounterableEnemy();
            foreach (var hit in colliders)
            {
                var arrow = hit.GetComponent<ArrowController>();
                if (arrow != null)
                {
                    SuccessfulCounterAttack();
                    arrow.FlipArrow(true);
                }
                Enemy.Enemy enemy = hit.GetComponent<Enemy.Enemy>();
                if ( enemy != null)
                {
                    if (enemy.CanBeStunned())
                    {
                        SuccessfulCounterAttack();
                        float xOffset;
                        if (Random.value > 0.5f)
                        {
                            xOffset = 1.5f;
                        }
                        else
                        {
                            xOffset = -1.5f;
                        }
                        SkillManager.Instance.cloneSkill.CreateCloneOnCounterAttack(enemy,new Vector3(xOffset,0));
                        if(SkillManager.Instance.parrySkill.parryRestoreUnlocked)
                            player.stats.currentHealth += player.stats.GetMaxHealth() * SkillManager.Instance.parrySkill.restorePercentage;
                    }
                }
                
            }
            
            if (stateTimer >= player.counterAttackDuration || triggerCalled )
            {
                stateMachine.ChangeState(player.idleState);
            }
            
        }

        private void SuccessfulCounterAttack()
        {
            stateTimer = -1; //状态停止 
            player.anim.SetBool("SuccessfulCounterAttack",true);
            player.stats.MakeInvincible(true);
            isSucceed = true;
        }

        public override void Exit()
        {
            base.Exit();
            if (isSucceed)
            {
                player.stats.MakeInvincible(false);
            }
        }
    }
}