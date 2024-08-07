﻿using Player.Universal;
using UnityEngine;

namespace Player.State
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        private int comboCounter;
        private float lastTimeAttacked;
        private float comboWindow = 2;
        public PlayerPrimaryAttackState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            {
                comboCounter = 0;
            }
            player.anim.SetInteger("ComboCounter",comboCounter);
        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled)
            {
                stateMachine.ChangeState(player.idleState);
            }

            if (stateTimer >= 0.15f)
            {
                player.rb.velocity = new Vector2(0, 0);
            }
        }

        public override void Exit()
        {
            base.Exit();
            comboCounter++;
            lastTimeAttacked = Time.time;
        }
    }
}