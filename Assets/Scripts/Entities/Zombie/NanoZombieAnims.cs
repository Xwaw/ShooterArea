using System;
using Entities.Zombie.States;
using Interfaces;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities.Zombie
{
    public class NanoZombieAnims : MonoBehaviour
    {
        [SerializeField] private AIController aiController;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        
        private bool _isFacingRight = true;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if(aiController == null)
                Debug.LogError("AI Controller is null");
            if(_animator == null)
                Debug.LogError("Animator is null");
            if(_spriteRenderer == null)
                Debug.LogError("SpriteRenderer is null");
        }
        public void Update()
        {
            switch (IsValueFacingRight())
            {
                case < 0 when _isFacingRight:
                case > 0 when !_isFacingRight:
                    Flip();
                    break;
            }

            var currentState = aiController.GetCurrentState();
            if (currentState is IAnimStates animState)
            {
                animState.ApplyAnimation(_animator);
            }
        }

        private float IsValueFacingRight()
        {
            var directionVector = aiController.Follow.GetDirection();
            return directionVector.normalized.x;
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;

            var scale = _spriteRenderer.transform.localScale;
            scale.x *= -1;
            _spriteRenderer.transform.localScale = scale;
        }
    }
    
    public enum ZombieAnimState
    {
        Idle = 0,
        Walk = 1,
        Run = 2,
        Attack = 3,
        Dead = 4,
        CrawlIdle = 5,
        CrawlWalk = 6,
        CrawlAttack = 7,
        CrawlDead = 8,
    }
}
