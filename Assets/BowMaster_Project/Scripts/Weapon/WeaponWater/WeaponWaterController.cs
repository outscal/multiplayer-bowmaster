using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponWaterController : WeaponController
    {
        public WeaponWaterController(WeaponService weaponService, float force
        , Vector2 direction
            , Vector2 spawnPos)
        {
            SetWeaponType();
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(
            GetWeaponView().gameObject);
            weapon.transform.position = spawnPos;
            weaponView = weapon.GetComponent<WeaponWaterView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
        }

        protected override void SetWeaponType()
        {
            weaponType = WeaponType.Water;
        }
    }
}