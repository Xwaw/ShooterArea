using Entities.Zombie.Components;
using Interfaces;
using UnityEngine;

namespace Entities.Zombie.States
{
    public class IdleState : IState, IAnimStates
    {
        private AIController _ai;

        public void Enter(AIController controller)
        {
            _ai = controller;
        }

        public void Update()
        {
            if (!_ai.Follow.HasTarget())
                _ai.Follow.SetTarget(_ai.Follow.target);

            if (_ai.Follow.HasTarget())
                _ai.SetState(new WalkState());
        }

        public void Exit() => Debug.Log("EXIT IDLE");

        public void ApplyAnimation(Animator animator)
        {
            animator.SetInteger("AnimState", (int)ZombieAnimState.Idle);
        }
    }

}