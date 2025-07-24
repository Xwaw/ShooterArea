using Player.MovementScripts;
using UnityEngine;

namespace Player.PlayerAnims
{
    public class PlayerAnims : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteGraphs;
        [SerializeField] private AnimationsManager animationsManager;
        [SerializeField] private MovementLogic movementLogic;
        private bool _isFacingRight = true;
    
        private void OnEnable()
        {
            animationsManager.OnMoveStateChanged += state => animator.SetBool("isMoving", animationsManager.IsMoving());
            animationsManager.OnJumpStateChanged += state => animator.SetBool("isJumping", animationsManager.IsJumping());
            animationsManager.OnFallStateChanged += state => animator.SetBool("isFalling", animationsManager.IsFalling());
            animationsManager.OnDuckStateChanged += state => animator.SetBool("isDucking", animationsManager.IsCrouching());
        }

        public void Update()
        {
            switch (movementLogic.moveInputValue)
            {
                case < 0 when _isFacingRight:
                case > 0 when !_isFacingRight:
                    Flip();
                    break;
            }
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;

            var scale = spriteGraphs.transform.localScale;
            scale.x *= -1;
            spriteGraphs.transform.localScale = scale;
        }
    }
}
