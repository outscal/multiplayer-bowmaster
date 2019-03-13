using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController
    {
        private WeaponService weaponService;
        private WeaponView weaponView;

        public WeaponController(WeaponService weaponService, float force
        , Vector2 direction
            , Vector2 spawnPos)
        {
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(
            weaponService.ReturnWeaponScriptable().weaponList[0].gameObject);
            weapon.transform.position = spawnPos;
            weaponView = weapon.GetComponent<WeaponView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
        }
    }
}