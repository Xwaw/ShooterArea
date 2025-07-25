using Entities.Interfaces;
using Entities.Zombie.Components;
using Interfaces;
using UnityEngine;

namespace Entities.Zombie.States
{
    public class IdleState : IState, IAnimStates
    {
        private AIController _ai;
        private bool _isAttacked;

        public void Enter(AIController controller)
        {
            _ai = controller;
            
            _ai.Follow.OnTargetLost += OnTargetLost;
            
            _isAttacked = false;
        }

        public void Update()
        {
            if (!_isAttacked && _ai.Health.CurrentHealth < _ai.Health.MaxHealth)
            {
                _isAttacked = true;
            }
            
            if (_ai.Follow.CurrentTarget == null) return;
            if(_isAttacked) _ai.SetState(new RunState());
            else _ai.SetState(new WalkState());
        }

        public void Exit()
        {
            _isAttacked = false;
        }

        public void ApplyAnimation(Animator animator)
        {
            animator.SetInteger("AnimState", (int)ZombieAnimState.Idle);
        }
        
        private void OnTargetLost()
        {
            _ai.SetState(new IdleState());
        }
    }
}