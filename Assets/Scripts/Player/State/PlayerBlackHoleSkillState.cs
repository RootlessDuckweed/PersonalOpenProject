using System.Drawing.Printing;
using Player.Skill;
using UnityEngine;

namespace Player.State
{
    public class PlayerBlackHoleSkillState : PlayerState
    {
        private float _flyTime = 0.4f;
        private bool _skillUsed;
        private float _originalGravity;
        
        public PlayerBlackHoleSkillState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        //////////////////////////////////////////////////
        /////we exit this state in BlackHoleController////
        //////////////////////////////////////////////////
        public override void Enter()
        {
            base.Enter();
            _skillUsed = false;
            _originalGravity = player.rb.gravityScale;
            player.rb.gravityScale = 0;
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < _flyTime)
            {
                player.rb.velocity = new Vector2(0, 15);
            }

            if (stateTimer > _flyTime)
            {
                player.rb.velocity = new Vector2(0, -0.1f);
                if (!_skillUsed) // if the skill not be used 
                {
                    // cast the skill
                  if(SkillManager.Instance.blackHoleSkill.CanUseSkill())
                        _skillUsed = true;
                }
            }
        }

        public override void Exit()
        {
            player.rb.gravityScale = _originalGravity;
            base.Exit();
        }



        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }
    }
}