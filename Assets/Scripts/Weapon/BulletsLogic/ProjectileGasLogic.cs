using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class ProjectileGasLogic : MonoBehaviour
{
    private float scale = 0.3f;
    private float speedOfSpread = 1.3f;
    private Vector3 shootDirection;
    private float damage;
    void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Init(Vector3 shootDirection, float damage)
    {
        this.shootDirection = shootDirection;
        this.damage = damage;
    }
    
    void Update()
    {
        transform.position += shootDirection * Time.deltaTime;
        transform.localScale += new Vector3(1, 1, 1) * scale * speedOfSpread * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }

        if (other.CompareTag("Bullet"))
        {
            return;
        }
        
        HealthEntityManager health = other.gameObject.GetComponent<HealthEntityManager>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
