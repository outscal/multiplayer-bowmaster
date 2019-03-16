using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSystem;

namespace WeaponSystem
{
    public class WeaponWaterController : WeaponController
    {
        public WeaponWaterController(WeaponService weaponService, float force
        , Vector2 direction, Vector2 spawnPos, ICameraService cameraService)
        {
            SetWeaponType();
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(
            GetWeaponView().gameObject);
            weapon.transform.position = spawnPos;
            weaponView = weapon.GetComponent<WeaponWaterView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
            cameraService.SetWeaponToFollow(weapon);
        }

        protected override void SetWeaponType()
        {
            weaponType = WeaponType.Water;
        }
    }
}