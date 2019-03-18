using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WeaponSystem
{
    public class WeaponAirController : WeaponController
    {
        readonly SignalBus signalBus;

        public WeaponAirController(WeaponService weaponService, float force
        , Vector2 direction, Vector2 spawnPos, SignalBus signalBus, bool localPlayer)
        {
            this.signalBus = signalBus;
            SetWeaponType();
            this.LocalPlayer = localPlayer;
            this.weaponService = weaponService;
            GameObject weapon = GameObject.Instantiate<GameObject>(
            GetWeaponView().gameObject);
            weapon.transform.position = spawnPos;
            weaponView = weapon.GetComponent<WeaponAirView>();
            weaponView.SetController(this);
            weaponView.Shoot(force, direction);
            signalBus.TryFire(new SignalSpawnWeapon() { weaponObject = weapon });
        }

        protected override void SetWeaponType()
        {
            weaponType = WeaponType.Air;
        }

    }
}