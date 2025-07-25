using System;
using System.Collections.Generic;
using Entities.Interfaces;
using Entities.Zombie.Components;
using Entities.Zombie.States;
using Interfaces;
using Player;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using IState = Interfaces.IState;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

namespace Entities.Zombie
{
    public class AIController : MonoBehaviour
    {
        private IState _currentState;
        public MovementComponent Movement { get; private set; }
        public FollowComponent Follow { get; private set; }
        public AttackComponent Attack { get; private set; }
        
        public IStats Stats { get; private set; }
        public IHealth Health { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<MovementComponent>();
            Follow = GetComponent<FollowComponent>();
            Attack = GetComponent<AttackComponent>();
            
            Stats = GetComponentInParent<IStats>();
            Health = GetComponentInParent<IHealth>();
        }

        private void Start()
        {
            SetState(new IdleState());
        }

        private void Update()
        {
            _currentState?.Update();
        }

        public void SetState(IState newState)
        {
            if (_currentState?.GetType() == newState.GetType()) return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter(this);
        }

        public IState GetCurrentState() => _currentState;
    }
}