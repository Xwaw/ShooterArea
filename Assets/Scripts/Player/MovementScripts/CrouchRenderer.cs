using UnityEngine;

namespace Entities.Player.MovementScripts
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
            if (_crouchLogic.isCrouching)
            {
                SetGraphicsOffset(true, spriteRenderer, spriteRendererAimWeapon);
            }
            else
            {
                SetGraphicsOffset(false, spriteRenderer, spriteRendererAimWeapon);
            }
        }
    
        private void SetGraphicsOffset(bool isCrouch, SpriteRenderer spritePlayer, SpriteRenderer spriteAimWeapon)
        {
            float yOffset = isCrouch ? 1.15f : 0.85f;
        
            spritePlayer.transform.localPosition = new Vector3(0, yOffset, 0);
            spriteAimWeapon.transform.localPosition = new Vector3(0, yOffset, 0); //AimWeapon means Sprite of weapon with Arms
        }
    }
}
