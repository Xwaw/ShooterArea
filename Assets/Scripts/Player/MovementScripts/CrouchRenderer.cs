using UnityEngine;

namespace Player.MovementScripts
{
    public class CrouchRenderer : MonoBehaviour
    {
        private CrouchLogic _crouchLogic;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer spriteRendererAimWeapon;
        private void Awake()
        {
            _crouchLogic = FindObjectOfType<CrouchLogic>();
            
            if(_crouchLogic == null)
                Debug.LogError("Crouch Logic in 'CrouchRenderer' Script not found");
            if(spriteRenderer == null)
                Debug.LogError("Sprite Renderer in 'CrouchRenderer' Script not found");
        }

        private void Update()
        {
            SetGraphicsOffset(_crouchLogic.isCrouching);
        }
    
        private void SetGraphicsOffset(bool isCrouch)
        {
            float yOffset = isCrouch ? 1.15f : 0.85f;
        
            spriteRenderer.transform.localPosition = new Vector3(0, yOffset, 0);
            spriteRendererAimWeapon.transform.localPosition = new Vector3(0, yOffset, 0); //AimWeapon means Sprite of weapon with Arms
        }
    }
}
