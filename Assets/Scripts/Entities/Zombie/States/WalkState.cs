using Interfaces;
using UnityEngine;

namespace Entities.Zombie.States
{
    public class WalkState : IState, IAnimStates
    {
        private AIController _ai;
        private float _speed;
        private bool _isAttacked;

        public void Enter(AIController controller)
        {
            _ai = controller;
            _speed = _ai.Stats.Speed * 0.25f;
        }

        public void Update()
        {
            if (_ai.Follow.HasTarget)
            {
                _ai.Movement.Move(_ai.Follow.GetDirection(), _speed);
            }
            
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
            animator.SetInteger("AnimState", (int)ZombieAnimState.Walk);
        }
    }
}