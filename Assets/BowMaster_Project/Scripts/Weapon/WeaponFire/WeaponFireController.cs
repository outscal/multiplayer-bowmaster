using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CameraSystem;

namespace WeaponSystem
{
    public class WeaponFireController : WeaponController
    {
        public WeaponFireController(WeaponService weaponService, float force
        , Vector2 direction, Vector2 spawnPos, ICameraService cameraService)
        {
            SetWeaponType();
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(GetWeaponView().gameObject);
            weapon.transform.position = spawnPos;
            weaponView = weapon.GetComponent<WeaponFireView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
            cameraService.SetWeaponToFollow(weapon);
        }

        protected override void SetWeaponType()
        {
            weaponType = WeaponType.Fire;
        }
    }
}