using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace WeaponSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponView : MonoBehaviour
    {
        private WeaponController weaponController;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private WeaponType weaponType;

        private void OnEnable()
        {
            Debug.Log("[WeaponView] WeaponSpawned");
        }

        public void SetController(WeaponController weaponController)
        {
            this.weaponController = weaponController;
        }

        public virtual void Shoot(float force, Vector2 direction)
        {
            if (force > 30)
                force = 30;
            rb.AddForce(force * direction, ForceMode2D.Impulse);
        }

        public WeaponType ReturnWeaponType()
        {
            return weaponType;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<ITakeDamage>() != null)
            {
                other.gameObject.GetComponent<ITakeDamage>().DamageAmount(51f);
            }

            DestroyWeapon();
        }

        private void DestroyWeapon()
        {
            weaponController.DestroyWeapon();
            Destroy(gameObject);
        }
    }
}