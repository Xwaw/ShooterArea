using Interfaces;
using UnityEngine;

namespace Entities.Zombie.States
{
    public class AttackState : IState, IAnimStates
    {
        private AIController _ai;
        private Animator _animator;
        private float _speed;

        public void Enter(AIController controller)
        {
            _ai = controller;
        }

        public void Update()
        {
            if (NanoZombieAnims.IsAttackingFrame)
            {
                _ai.Attack.TryAttack(_ai);
                NanoZombieAnims.IsAttackingFrame = false;
            }

            if (NanoZombieAnims.IsAnimEnd)
            {
                _ai.SetState(new IdleState());
                NanoZombieAnims.IsAnimEnd = false;
            }
        }

        public void Exit(){}
        
        public void ApplyAnimation(Animator animator)
        {
            _animator = animator;
            animator.SetInteger("AnimState", (int)ZombieAnimState.Attack);
        }
    }
}