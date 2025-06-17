using UnityEngine;

public class BulletRender : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private WeaponController weaponController;
    private float displayTime = 0.05f;
    private float timer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponController = GetComponentInParent<WeaponController>();
    }

    void Start()
    {
        if (weaponController != null)
        {
            weaponController.weaponFired += OnShot;
        }
    }

    public void OnShot()
    {
        spriteRenderer.color = Color.white;
        timer = displayTime;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                spriteRenderer.color = Color.clear;
            }
        }
    }
}