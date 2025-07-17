using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CrouchLogic : MonoBehaviour
{
    public event Action OnCrouch;
    
    private BoxCollider2D _collider2D;
    private Rigidbody2D _rb;
    private JumpLogic _jumpLogic;
    
    private Vector2 standingSize;
    private Vector2 crouchingSize;
    
    private Vector2 standingOffset;
    private Vector2 crouchingOffset;
    
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
        standingSize = _collider2D.size;
        crouchingSize = new Vector2(_collider2D.size.x, _collider2D.size.y / 2);
        
        standingOffset = _collider2D.offset;
        crouchingOffset = new Vector2(_collider2D.offset.x, standingOffset.y + (standingSize.y - crouchingSize.y) / 2);
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
            
            boxCollider2D.size = crouchingSize;
            boxCollider2D.offset = crouchingOffset;
            
            isCrouching = true;
        }
        else
        {
            boxCollider2D.size = standingSize;
            boxCollider2D.offset = standingOffset;
            
            _rb.transform.position = new Vector3(_rb.transform.position.x, _rb.transform.position.y + crouchingOffset.y + crouchingSize.y, _rb.transform.position.z);
            transform.localPosition = new Vector3(0f, -boxCollider2D.size.y / 2f, 0f);

            isCrouching = false;
        }
    }
}
