using Entities.Player;
using Player;
using Scriptable_Objects_Data.DataStores;
using UnityEngine;
using Weapons.Weapon;

namespace Weapons.Appearance
{
    public class WeaponRender : MonoBehaviour
    {
        private Camera _cam;
        private SpriteRenderer _spriteRenderer;

        private Vector3 _aimTarget;
        private Vector3 _scale;
        private float _aimPoint;
    
        private InventoryManager _inventoryManager;
        private WeaponController _weaponController;

        [SerializeField] private float recoilStrength = 2.5f;
        [SerializeField] private float maxRecoilAngle = 10f;
        [SerializeField] private float recoilRecoverySpeed = 15f;
        [SerializeField] private float recoilBuildUpSpeed = 70f;

        private float _targetRecoilAngle;
        private float _currentRecoilAngle;
        private float _recoilVelocity;

        private int _flippedWep;

        public void Awake()
        {
            _scale = transform.localScale;
            _cam = Camera.main;

            _inventoryManager = GetComponentInParent<InventoryManager>();
            _weaponController = GetComponentInParent<WeaponController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GetBarrelTransform = transform.Find("BarrelPosition");

            _inventoryManager.OnWeaponChanged += renderWeaponSpriteOnSwitch;
        }

        private void Start()
        {
            renderWeaponSpriteOnSwitch(_inventoryManager.GetCurrentWeapon().GetWeaponData);
        }

        private void Update()
        {
            if (!_inventoryManager.GetCurrentWeapon().GetWeaponData.ignoreRecoilAnimation)
            {
                UpdateRecoil();
            }
            RotateWeaponByAimTarget(_currentRecoilAngle);
        
            FlipWeaponByView();
        }

        private void renderWeaponSpriteOnSwitch(WeaponData currentWeapon)
        {
            _spriteRenderer.sprite = currentWeapon.weaponAimSprite;
            GetBarrelTransform.localPosition = currentWeapon.offsetBarrel;
        }
    
        public Transform GetBarrelTransform { get; private set; }

        private void UpdateRecoil()
        {
            if (_weaponController.CanShootThisFrame && _inventoryManager.GetCurrentWeapon().MagazineAmmo > 0)
            {
                _targetRecoilAngle += recoilStrength;
                _targetRecoilAngle = Mathf.Min(_targetRecoilAngle,
                    (float)_inventoryManager.GetCurrentWeapon().GetWeaponData.recoil * maxRecoilAngle);
            }
            else
            {
                _targetRecoilAngle = Mathf.MoveTowards(_targetRecoilAngle, 0f, Time.deltaTime * recoilRecoverySpeed);
            }

            _currentRecoilAngle = Mathf.Lerp(_currentRecoilAngle, _targetRecoilAngle, Time.deltaTime * recoilBuildUpSpeed);
        }

        private void RotateWeaponByAimTarget(float recoilAngle)
        {
            _aimPoint = GetAimAngle();
            transform.localRotation = Quaternion.Euler(0, 0, _aimPoint + recoilAngle * _flippedWep);
        }

        private float GetAimAngle()
        {
            _aimTarget = _cam.ScreenToWorldPoint(Input.mousePosition);
            _aimTarget.z = 0;

            Vector2 direction = (_aimTarget - transform.position).normalized;
            transform.localScale = new Vector3(_scale.x, _scale.y * _flippedWep, _scale.z);

            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        private void FlipWeaponByView()
        {
            _flippedWep = (_aimTarget.x < transform.position.x) ? -1 : 1;
        }
    }
}
