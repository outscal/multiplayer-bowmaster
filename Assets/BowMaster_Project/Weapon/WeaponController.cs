using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController
    {
        private WeaponService weaponService;
        private WeaponView weaponView;

        public WeaponController(WeaponService weaponService, float force, Vector2 direction)
        {
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(
            weaponService.ReturnWeaponScriptable().weaponList[0].gameObject);

            weaponView = weapon.GetComponent<WeaponView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
        }
    }
}