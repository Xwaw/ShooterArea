using Entities.Player;
using UnityEngine;

public class WeaponRender : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer spriteRenderer;
    private Transform barrelOffset;

    private Vector3 aimTarget;
    private Vector3 scale;
    private float aimPoint;
    
    private InventoryManager inventoryManager;
    private WeaponController weaponController;

    [SerializeField] private float recoilStrength = 2.5f;
    [SerializeField] private float maxRecoilAngle = 10f;
    [SerializeField] private float recoilRecoverySpeed = 15f;
    [SerializeField] private float recoilBuildUpSpeed = 70f;

    private float targetRecoilAngle;
    private float currentRecoilAngle;
    private float recoilVelocity;

    private int flippedWep;
    void Awake()
    {
        scale = transform.localScale;
        cam = Camera.main;

        inventoryManager = GetComponentInParent<InventoryManager>();
        weaponController = GetComponentInParent<WeaponController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        barrelOffset = transform.Find("BarrelPosition");

        inventoryManager.OnWeaponChanged += renderWeaponSpriteOnSwitch;
    }

    void Start()
    {
        renderWeaponSpriteOnSwitch(inventoryManager.GetCurrentWeapon());
    }

    void Update()
    {
        UpdateRecoil();
        RotateWeaponByAimTarget(currentRecoilAngle);
        FlipWeaponByView();
    }

    private void renderWeaponSpriteOnSwitch(WeaponData currentWeapon)
    {
        spriteRenderer.sprite = currentWeapon.weaponAimSprite;
        barrelOffset.localPosition = currentWeapon.offsetBarrel;
    }
    
    public Transform GetBarrelTransform => barrelOffset; 

    private void UpdateRecoil()
    {
        if (weaponController.canShootThisFrame)
        {
            targetRecoilAngle += recoilStrength;
            targetRecoilAngle = Mathf.Min(targetRecoilAngle, (float) inventoryManager.GetCurrentWeapon().recoil * maxRecoilAngle);
        }
        else
        {
            targetRecoilAngle = Mathf.MoveTowards(targetRecoilAngle, 0f, Time.deltaTime * recoilRecoverySpeed);
        }

        currentRecoilAngle = Mathf.Lerp(currentRecoilAngle, targetRecoilAngle, Time.deltaTime * recoilBuildUpSpeed);
    }

    private void RotateWeaponByAimTarget(float recoilAngle)
    {
        aimPoint = GetAimAngle();
        transform.localRotation = Quaternion.Euler(0, 0, aimPoint + recoilAngle * flippedWep);
    }

    private float GetAimAngle()
    {
        aimTarget = cam.ScreenToWorldPoint(Input.mousePosition);
        aimTarget.z = 0;

        Vector2 direction = (aimTarget - transform.position).normalized;
        transform.localScale = new Vector3(scale.x, scale.y * flippedWep, scale.z);

        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void FlipWeaponByView()
    {
        flippedWep = (aimTarget.x < transform.position.x) ? -1 : 1;
    }
}
