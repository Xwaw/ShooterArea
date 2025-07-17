using Effects;
using UnityEngine;
using Weapons.Projectiles.BulletComponents.Physic;

namespace Core.Behaviours
{
    public class LandMineBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private DeployPhysicProjectile deployPhysicProjectile;
        private bool _isMined = false;
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                LaunchLandMine();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Ground")) return;
            
            LaunchLandMine();
            
            _isMined = true;
        }

        private void LaunchLandMine()
        {
            if (!_isMined) return;
            var explosionObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            var script = explosionObject.GetComponent<ExplosionEffect>();
            script.Init(deployPhysicProjectile.damage);
            
            Destroy(gameObject);
        }
    }
}
