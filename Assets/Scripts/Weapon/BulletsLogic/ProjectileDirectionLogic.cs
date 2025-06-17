using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileDirectionLogic : MonoBehaviour
{
   private Vector3 direction;
   private float speed;
   private float damage;
   
   public Collider2D hitted;
   

   public void Init(float speed, Vector3 direction, float damage)
   {
       this.speed = speed;
       this.direction = direction.normalized;
       
       this.damage = damage;
   }

   void Update()
   {
       transform.position += direction * speed * Time.deltaTime;
   }
   
   void OnTriggerEnter2D(Collider2D other)
   {
       if (other.tag == "Player")
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
