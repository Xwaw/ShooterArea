using Interfaces;
using UnityEngine;

namespace Entities.Zombie.States
{
    public class WalkState : IState, IAnimStates
    {
        private AIController _ai;
        private float _speed;

        public void Enter(AIController controller)
        {
            _ai = controller;
            _speed = _ai.Stats.Speed * 0.25f;
        }

        public void Update()
        {
            if (!_ai.Follow.HasTarget())
            {
                _ai.SetState(new IdleState());
                return;
            }

            _ai.Movement.Move(_ai.Follow.GetDirection(), _speed);
        }

        public void Exit() => Debug.Log("EXIT WALK");

        public void ApplyAnimation(Animator animator)
        {
            
            animator.SetInteger("AnimState", (int)ZombieAnimState.Walk);
        }
    }
}