using System.Collections.Generic;
using System.Drawing;
using Entities.Interfaces;
using UnityEngine;
using Color = UnityEngine.Color;

public class MovementManager : MonoBehaviour {
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private StatManager _statMain;

    [Header("Movement")]
    [SerializeField] private GameObject groundChecker;
    [SerializeField] private SpriteRenderer playerGraphics;
    [SerializeField] private SpriteRenderer weaponSprite;
    
    private float moveInput;
    private bool shouldJump;
    public bool isGrounded;
    private bool isCrouching;

    private Vector2 standingSize;
    private Vector2 crouchingSize;
    
    private Vector2 standingOffset;
    private Vector2 crouchingOffset;

    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        _statMain = GetComponent<StatManager>();
    }
    void Start()
    {
        standingSize = box.size;
        crouchingSize = new Vector2(box.size.x, box.size.y / 2);
        
        standingOffset = box.offset;
        crouchingOffset = new Vector2(box.offset.x, standingOffset.y + (standingSize.y - crouchingSize.y) / 2);
    }

    void Update() {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.W) && isGrounded ) shouldJump = true;
        
        ToggleCrouching();
        
        //Debug.DrawRay(transform.position, Vector2.down * 2f, Color.red);
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(moveInput * _statMain.GetMovementSpeed(), rb.velocity.y);

        isGrounded = Physics2D.Raycast(groundChecker.transform.position, Vector2.down, 0.02f);

        if (shouldJump && !isCrouching )
        {
            rb.AddForce(new Vector2(0f, _statMain.GetJumpForce()), ForceMode2D.Impulse);
            shouldJump = false;
            isGrounded = false;
        }
    }

    private void ToggleCrouching()
    {
        if(Input.GetKey(KeyCode.S) && !isMoving() && isGrounded)
        {
            lowerGraphicsOfPlayer(1.15f, playerGraphics, weaponSprite);
            setLowerBoxCollider(box);
        }
        if (Input.GetKeyUp(KeyCode.S) && !isMoving() && isGrounded && isCrouching)
        {
            lowerGraphicsOfPlayer(0.88f, playerGraphics, weaponSprite);
            setLowerBoxCollider(box, false);
        }
    }

    private void setLowerBoxCollider(BoxCollider2D boxCollider2D, bool lower = true)
    {
        if (lower)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f);
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            groundChecker.transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            
            boxCollider2D.size = crouchingSize;
            boxCollider2D.offset = crouchingOffset;
            
            isCrouching = true;
        }
        else
        {
            boxCollider2D.size = standingSize;
            boxCollider2D.offset = standingOffset;


            transform.position = new Vector3(transform.position.x, transform.position.y + crouchingOffset.y + crouchingSize.y, transform.position.z);
            groundChecker.transform.localPosition = new Vector3(0f, -boxCollider2D.size.y / 2f, 0f);

            isCrouching = false;
        }
    }

    private void lowerGraphicsOfPlayer(float yOffset, SpriteRenderer player, SpriteRenderer weapon)
    {
        player.transform.localPosition = new Vector3(0, yOffset, 0);
        weapon.transform.localPosition = new Vector3(0, yOffset, 0);
    }

    public bool isMoving() {
        return Mathf.Abs(moveInput) > 0.1f && isGrounded && !isCrouching;
    }

    public bool isFalling()
    {
        return !isGrounded && rb.velocity.y < 0f;
    }

    public Vector3 getInput()
    {  
        return new Vector3(moveInput, 0f, 0f);
    }

    public bool isJumping()
    {
        return !isGrounded && rb.velocity.y > 0f;
    }

    public bool isDucking()
    {
        return isGrounded && !isMoving() && isCrouching;
    }
}