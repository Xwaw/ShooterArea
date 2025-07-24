using System;
using UnityEngine;

namespace Entities.Zombie.Components
{
    public class FollowComponent : MonoBehaviour
    {
        public Transform target;
        public float rangeSight = 5f;
        public float forgetMultiplier = 1.5f;

        public Vector2 GetDirection()
        {
            if (target == null) return Vector2.zero;
            return (target.position - transform.position).normalized;
        }

        public void SetTarget(Transform newTarget) => target = newTarget;

        public bool HasTarget() => target != null;

        private void Update()
        {
            if (target == null) return;

            float distance = Vector2.Distance(transform.position, target.position);
            if (distance > rangeSight * forgetMultiplier)
                target = null;
        }
    }

}