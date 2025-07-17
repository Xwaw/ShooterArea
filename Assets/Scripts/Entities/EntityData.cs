using UnityEngine;

namespace Entities
{
    public class EntityData : MonoBehaviour
    {
        [SerializeField] protected float damage;
        [SerializeField] protected float attackSpeed;
        [SerializeField] protected float defense;
        
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;
    }
}