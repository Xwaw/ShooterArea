using System;
using UnityEngine;

namespace Entities.Zombie.Components
{
    public class FollowComponent : MonoBehaviour
    {
        [Header("Settings")]
        public float rangeSight = 5f;
        [Range(1f, 3f)] public float forgetMultiplier = 1.5f;

        [Header("Target")]
        [SerializeField] private Transform candidateTarget;

        public Transform CurrentTarget { get; set; }

        public event Action<Transform> OnTargetAcquired;
        public event Action OnTargetLost;

        public bool HasTarget => CurrentTarget != null;
        
        public Vector2 GetDirection()
        {
            if (CurrentTarget == null) return Vector2.zero;
            return (CurrentTarget.position - transform.position).normalized;
        }

        private void SetTarget(Transform newTarget)
        {
            if (newTarget == CurrentTarget) return;
            CurrentTarget = newTarget;
            OnTargetAcquired?.Invoke(newTarget);
        }

        private void ClearTarget()
        {
            if (CurrentTarget == null) return;
            CurrentTarget = null;
            OnTargetLost?.Invoke();
        }

        private void Update()
        {
            if (TryAcquireTarget(candidateTarget))
            {
                CurrentTarget = candidateTarget;
            }
            if (!HasTarget) return;

            var distance = Vector2.Distance(transform.position, CurrentTarget.position);
            if (distance > rangeSight * forgetMultiplier)
            {
                ClearTarget();
            }
        }

        private bool IsInSight(Transform candidate)
        {
            float distance = Vector2.Distance(transform.position, candidate.position);
            return distance <= rangeSight;
        }

        private bool TryAcquireTarget(Transform candidate)
        {
            if (IsInSight(candidate))
            {
                SetTarget(candidate);
                return true;
            }

            return false;
        }
    }
}