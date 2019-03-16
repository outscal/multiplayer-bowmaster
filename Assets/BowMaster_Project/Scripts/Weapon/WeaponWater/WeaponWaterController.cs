using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSystem;
using Zenject;

namespace WeaponSystem
{
    public class WeaponWaterController : WeaponController
    {
        readonly SignalBus signalBus;

        public WeaponWaterController(WeaponService weaponService, float force
        , Vector2 direction, Vector2 spawnPos, SignalBus signalBus)
        {
            SetWeaponType();
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(
            GetWeaponView().gameObject);
            weapon.transform.position = spawnPos;
            weaponView = weapon.GetComponent<WeaponWaterView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
            signalBus.TryFire(new SignalSpawnWeapon() { weaponObject = weapon });
        }

        protected override void SetWeaponType()
        {
            weaponType = WeaponType.Water;
        }
    }
}