using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnims : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [FormerlySerializedAs("playerMovement")] [FormerlySerializedAs("movement")] [SerializeField] private MovementManager movementManager;
    [SerializeField] private SpriteRenderer spriteGraphs;
    // Update is called once per frame
    void Update()
    {
         animator.SetBool("isMoving", movementManager.isMoving());
         animator.SetBool("isJumping", movementManager.isJumping());
         animator.SetBool("isFalling", movementManager.isFalling());
         animator.SetBool("isDucking", movementManager.isDucking());

         if (movementManager.getInput().x > 0f)
         {
             spriteGraphs.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
         }
         else if(movementManager.getInput().x < 0f)
         {
             spriteGraphs.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
         }
    }
}
