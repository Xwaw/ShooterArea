using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class ProjectilePhysicsLogic : MonoBehaviour
{
    private Vector3 directionEnd;
    private float velocitySpeed;
    private float damage;
    
    private Rigidbody2D rb2d;
    
    public void Init(Vector3 directionEnd, float velocitySpeed, float damage)
    {
        this.directionEnd = directionEnd;
        this.velocitySpeed = velocitySpeed;
        this.damage = damage;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(directionEnd.x * velocitySpeed, directionEnd.y * velocitySpeed);
    }
    
    private void Update()
    {
        RotateToVelocity();
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    private void RotateToVelocity()
    {
        Vector2 velocity = rb2d.velocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
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
