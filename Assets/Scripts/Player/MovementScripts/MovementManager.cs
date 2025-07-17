using System;
using System.Collections.Generic;
using System.Drawing;
using Entities.Interfaces;
using UnityEngine;
using Color = UnityEngine.Color;

public class MovementManager : MonoBehaviour {
    private MovementLogic _movementLogic;
    private CrouchLogic _crouchLogic;
    private JumpLogic _jumpLogic;
    private Rigidbody2D _rbOfPlayer;
    
    public event Action<bool> OnMoveStateChanged;
    public event Action<bool> OnJumpStateChanged;
    public event Action<bool> OnFallStateChanged;
    public event Action<bool> OnDuckStateChanged;

    private bool lastMoving;
    private bool lastJumping;
    private bool lastFalling;
    private bool lastDucking;
    
    private void Awake()
    {
        _movementLogic = GetComponent<MovementLogic>();
        _crouchLogic = GetComponent<CrouchLogic>();
        _jumpLogic = GetComponent<JumpLogic>();
        _rbOfPlayer = GetComponentInParent<Rigidbody2D>();
        
        if(_movementLogic == null)
            Debug.LogError("MovementLogic in 'MovementManager' Script not found");
        if(_crouchLogic == null)
            Debug.LogError("CrouchLogic in 'MovementManager' Script not found");
        if(_jumpLogic == null)
            Debug.LogError("JumpLogic in 'MovementManager' Script not found");
        if(_rbOfPlayer == null)
            Debug.LogError("RbOfPlayer in 'MovementManager' Script not found");
    }
    private void Update()
    {
        bool moving = IsMoving();
        if (moving != lastMoving)
        {
            lastMoving = moving;
            OnMoveStateChanged?.Invoke(moving);
        }

        bool jumping = IsJumping();
        if (jumping != lastJumping)
        {
            lastJumping = jumping;
            OnJumpStateChanged?.Invoke(jumping);
        }

        bool falling = IsFalling();
        if (falling != lastFalling)
        {
            lastFalling = falling;
            OnFallStateChanged?.Invoke(falling);
        }

        bool ducking = IsCrouching();
        if (ducking != lastDucking)
        {
            lastDucking = ducking;
            OnDuckStateChanged?.Invoke(ducking);
        }
    }
    public bool IsMoving() => Mathf.Abs(_movementLogic.moveInputValue) > 0.1f && _jumpLogic.IsOnGround && !_crouchLogic.isCrouching;
    public bool IsJumping() => !_jumpLogic.IsOnGround && _rbOfPlayer.velocity.y > 0f;
    public bool IsFalling() => !_jumpLogic.IsOnGround && _rbOfPlayer.velocity.y < 0f;
    public bool IsCrouching() => _jumpLogic.IsOnGround && !IsMoving() && _crouchLogic.isCrouching;
}