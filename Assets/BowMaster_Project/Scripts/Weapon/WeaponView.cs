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

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<ITakeDamage>() != null)
            {
                other.GetComponent<ITakeDamage>().DamageAmount(15f);
            }
        }
    }
}