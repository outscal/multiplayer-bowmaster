using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CameraSystem;

namespace WeaponSystem
{
    public class WeaponFireController : WeaponController
    {
        readonly SignalBus signalBus;

        public WeaponFireController(WeaponService weaponService, float force
        , Vector2 direction, Vector2 spawnPos, SignalBus signalBus, bool localPlayer)
        {
            SetWeaponType();
            this.LocalPlayer = localPlayer;
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(GetWeaponView().gameObject);
            weapon.transform.position = spawnPos;
            weaponView = weapon.GetComponent<WeaponFireView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
            signalBus.TryFire(new SignalSpawnWeapon() { weaponObject = weapon });
        }

        protected override void SetWeaponType()
        {
            weaponType = WeaponType.Fire;
        }
    }
}