using Interfaces;
using UnityEngine;

namespace Entities.Zombie.States
{
    public class RunState : IState, IAnimStates
    {
        private AIController _ai;
        private float _speed;

        public void Enter(AIController controller)
        {
            _ai = controller;
            _speed = _ai.Stats.Speed;
        }

        public void Update()
        {
            _ai.Movement.Move(_ai.Follow.GetDirection(), _speed);
            _ai.Attack.TryAttack(_ai);
        }

        public void Exit() => Debug.Log("EXIT RUN");

        public void ApplyAnimation(Animator animator)
        {
            animator.SetInteger("AnimState", (int)ZombieAnimState.Run);
        }
    }
}