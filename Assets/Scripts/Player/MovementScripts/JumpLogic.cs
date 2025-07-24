using System;
using UnityEngine;

namespace Player.MovementScripts
{
    public class JumpLogic : MonoBehaviour
    {
        private StatManager _statManager;
        private Rigidbody2D _rb;
        private CrouchLogic _crouchLogic;
        private BoxCollider2D _boxColliderOfPlayer;

        private bool _shouldJump;

        private Vector2 _overlapBoxSize;

        public bool IsOnGround { get; private set; }

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float raycastHeight = 0.1f;
        [SerializeField] private float raycastWidght = 1.35f;
    
        [SerializeField] private PhysicsMaterial2D groundFriction;
        [SerializeField] private PhysicsMaterial2D airFriction;
        private PhysicsMaterial2D _currentMaterial;

        private void Awake()
        {
            _statManager = GetComponentInParent<StatManager>();
            _rb = GetComponentInParent<Rigidbody2D>();
            _crouchLogic = GetComponent<CrouchLogic>();
            _boxColliderOfPlayer = GetComponentInParent<BoxCollider2D>();
        
            _overlapBoxSize = new Vector2(_boxColliderOfPlayer.size.x * raycastWidght, raycastHeight);
        
            if(_statManager == null)
                Debug.LogError("StatManager in 'JumpLogic' Script not found");
            if(_rb == null)
                Debug.LogError("Rigidbody in 'JumpLogic' Script not found");
            if(_crouchLogic == null)
                Debug.LogError("CrouchLogic in 'JumpLogic' Script not found");
            if(_boxColliderOfPlayer == null)
                Debug.LogError("BoxCollider2DOfPlayer in 'JumpLogic' Script not found");
        }
        private void FixedUpdate()
        {
            IsOnGround = Physics2D.OverlapBox(transform.position, _overlapBoxSize, 0f,groundMask);
        
            var targetMaterial = IsOnGround ? groundFriction : airFriction;
        
            if (_currentMaterial != targetMaterial)
            {
                _boxColliderOfPlayer.sharedMaterial = targetMaterial;
                _currentMaterial = targetMaterial;
            }
        
            if (_shouldJump && IsOnGround && !_crouchLogic.isCrouching)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0f); 
                _rb.AddForce(Vector2.up * _statManager.JumpForce, ForceMode2D.Impulse);
            
                IsOnGround = false;
            }
        
            _shouldJump = false;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.W)) _shouldJump = true;
        }
    
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsOnGround ? Color.green : Color.red;
            Gizmos.DrawWireCube(transform.position, _overlapBoxSize);
        }
    }
}
