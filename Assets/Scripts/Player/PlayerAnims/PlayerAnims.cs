using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerAnims : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteGraphs;
    [SerializeField] private MovementManager movementManager;
    [SerializeField] private MovementLogic movementLogic;
    private bool _isFacingRight = true;
    
    private void OnEnable()
    {
        movementManager.OnMoveStateChanged += state => animator.SetBool("isMoving", movementManager.IsMoving());
        movementManager.OnJumpStateChanged += state => animator.SetBool("isJumping", movementManager.IsJumping());
        movementManager.OnFallStateChanged += state => animator.SetBool("isFalling", movementManager.IsFalling());
        movementManager.OnDuckStateChanged += state => animator.SetBool("isDucking", movementManager.IsCrouching());
    }
    void Update()
    {
        if (movementLogic.moveInputValue < 0 && _isFacingRight)
        {
            Flip();
        }
        else if (movementLogic.moveInputValue > 0 && !_isFacingRight)
        {
            Flip();
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
