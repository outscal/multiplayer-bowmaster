using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CameraSystem;

namespace WeaponSystem
{
    public class WeaponService : IWeaponService
    {
        readonly SignalBus signalBus;
        private ScriptableObjWeapon scriptableObjWeapon;
        private WeaponController weaponController;
        private ICameraService cameraService;

        public WeaponService(SignalBus signalBus, ScriptableObjWeapon scriptableObjWeapon
        , ICameraService cameraService)
        {
            this.signalBus = signalBus;
            this.scriptableObjWeapon = scriptableObjWeapon;
            this.cameraService = cameraService;
            signalBus.Subscribe<SignalDestroyWeapon>(DestroyWeapon);
        }

        public void SpawnWeapon(float power, float angle, Vector2 spawnPos, WeaponType weaponType)
        {
            Vector2 direction = new Vector2((float)Mathf.Cos((angle) * Mathf.Deg2Rad),
                                            (float)Mathf.Sin((angle) * Mathf.Deg2Rad));

            if (weaponType == WeaponType.Air)
                weaponController = new WeaponAirController(this, power, direction, spawnPos,
                cameraService);
            else if (weaponType == WeaponType.Water)
                weaponController = new WeaponWaterController(this, power, direction, spawnPos,
                cameraService);
            else if (weaponType == WeaponType.Fire)
                weaponController = new WeaponFireController(this, power, direction, spawnPos,
                cameraService);
        }

        public ScriptableObjWeapon ReturnWeaponScriptable()
        {
            return scriptableObjWeapon;
        }

        public SignalBus GetSignalBus()
        {
            return signalBus; 
        }

        public void DestroyWeapon(SignalDestroyWeapon weaponDestroy)
        {
            weaponDestroy.weaponController = null;
        }
    }
}