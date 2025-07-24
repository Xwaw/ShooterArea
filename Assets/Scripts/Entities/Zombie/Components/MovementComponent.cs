using UnityEngine;

namespace Entities.Zombie.Components
{
    public class MovementComponent : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody2D>();
            if(_rigidbody == null)
                Debug.LogError("No rigidbody attached");
        }
        
        public void Move(Vector2 direction, float speed)
        {
            _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);
        }
    }
}