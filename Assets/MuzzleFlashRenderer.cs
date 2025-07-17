using System;
using Entities.Player;
using Player;
using UnityEngine;
using Weapons.Weapon;
using Random = UnityEngine.Random;

public class MuzzleFlashRenderer : MonoBehaviour
{
    [SerializeField] private int disappearDelay;
    
    [SerializeField] private Sprite[] spritesMuzzleFlashes;
    private SpriteRenderer _spriteRenderer;
    private WeaponController _weaponController;
    
    private int _alphaOfSprite;
    private bool _isFlashing;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weaponController = GetComponentInParent<WeaponController>();

        if (spritesMuzzleFlashes == null)
        {
            throw new ArgumentNullException(nameof(spritesMuzzleFlashes));
        }
        
        SetRandomMuzzleFlash();
        
        _weaponController.WeaponFired += ShowMuzzleFlash;
        
        _alphaOfSprite = 0;
    }

    private void Update()
    {
        if (_isFlashing)
        {
            _alphaOfSprite = 255;
            SetRandomMuzzleFlash();
            _isFlashing = false;
        }

        if (_alphaOfSprite <= 0) return;

        _alphaOfSprite -= (int)(Time.deltaTime * disappearDelay); 

        _alphaOfSprite = Mathf.Clamp(_alphaOfSprite, 0, 255);

        _spriteRenderer.color = new Color(1f, 1f, 1f, _alphaOfSprite / 255f);
    }


    private void SetRandomMuzzleFlash()
    {
        _spriteRenderer.sprite = spritesMuzzleFlashes[Random.Range(0, spritesMuzzleFlashes.Length)];
    }

    private void ShowMuzzleFlash()
    {
        _isFlashing = true;
    }
}
