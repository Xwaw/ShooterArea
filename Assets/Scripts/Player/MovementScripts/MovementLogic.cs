using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementLogic : MonoBehaviour
{
    private bool _isMoving;
    
    private StatManager _statManager;
    private Rigidbody2D _rb;
    private CrouchLogic _crouchLogic;

    public float moveInputValue;
    private void Awake()
    {
        _statManager = GetComponentInParent<StatManager>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _crouchLogic = GetComponent<CrouchLogic>();
            
        if(_statManager == null)
            Debug.LogError("StatManager in 'MovementLogic' Script not found");
        if(_rb == null)
            Debug.LogError("Rigidbody in 'MovementLogic' Script not found");
        if (_crouchLogic == null)
            Debug.LogError("CrouchLogic in 'MovementLogic' Script not found");
    }

    private void Update()
    {
        moveInputValue = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        float moveSpeed = _crouchLogic.isCrouching ? 0f : moveInputValue * _statManager.Speed;
        _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
    }
}
