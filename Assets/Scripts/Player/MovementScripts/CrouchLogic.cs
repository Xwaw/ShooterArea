using System;
using UnityEngine;

namespace Player.MovementScripts
{
    public class CrouchLogic : MonoBehaviour
    {
        public event Action OnCrouch;
    
        private BoxCollider2D _collider2D;
        private Rigidbody2D _rb;
        private JumpLogic _jumpLogic;
    
        private Vector2 _standingSize;
        private Vector2 _crouchingSize;
    
        private Vector2 _standingOffset;
        private Vector2 _crouchingOffset;
    
        public bool isCrouching;

        void Awake()
        {
            _collider2D = GetComponentInParent<BoxCollider2D>();
            _jumpLogic = GetComponentInParent<JumpLogic>();
            _rb = GetComponentInParent<Rigidbody2D>();
        
            if(_collider2D == null)
                Debug.LogError("collider2D in 'CrouchLogic' Script not found");
            if(_jumpLogic == null)
                Debug.LogError("jumpLogic in 'CrouchLogic' Script not found");
            if (_rb == null)
                Debug.LogError("rb in 'CrouchLogic' Script not found");
        
        }
        void Start()
        {
            _standingSize = _collider2D.size;
            _crouchingSize = new Vector2(_collider2D.size.x, _collider2D.size.y / 2);
        
            _standingOffset = _collider2D.offset;
            _crouchingOffset = new Vector2(_collider2D.offset.x, _standingOffset.y + (_standingSize.y - _crouchingSize.y) / 2);
        }

        void Update() {
            ToggleCrouching();
        }
    
        private void ToggleCrouching()
        {
            if(Input.GetKey(KeyCode.S) && _jumpLogic.IsOnGround)
            {
                setLowerBoxCollider(_collider2D);
            
                OnCrouch?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.S) && _jumpLogic.IsOnGround && isCrouching)
            {
                setLowerBoxCollider(_collider2D, false);
            }
        }
        private void setLowerBoxCollider(BoxCollider2D boxCollider2D, bool lower = true)
        {
            if (lower)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f);
                _rb.transform.position = new Vector3(_rb.transform.position.x, hit.point.y, _rb.transform.position.z);
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            
                boxCollider2D.size = _crouchingSize;
                boxCollider2D.offset = _crouchingOffset;
            
                isCrouching = true;
            }
            else
            {
                boxCollider2D.size = _standingSize;
                boxCollider2D.offset = _standingOffset;
            
                _rb.transform.position = new Vector3(_rb.transform.position.x, _rb.transform.position.y + _crouchingOffset.y + _crouchingSize.y, _rb.transform.position.z);
                transform.localPosition = new Vector3(0f, -boxCollider2D.size.y / 2f, 0f);

                isCrouching = false;
            }
        }
    }
}
