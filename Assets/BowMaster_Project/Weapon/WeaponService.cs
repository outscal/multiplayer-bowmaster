using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WeaponSystem
{
    public class WeaponService : IWeaponService
    {
        readonly SignalBus signalBus;
        private ScriptableObjWeapon scriptableObjWeapon;
        private WeaponController weaponController;

        public WeaponService(SignalBus signalBus, ScriptableObjWeapon scriptableObjWeapon)
        {
            this.signalBus = signalBus;
            this.scriptableObjWeapon = scriptableObjWeapon;
        }

        public void SpawnWeapon(float power, float angle, Vector2 spawnPos)
        {
            Vector2 direction = new Vector2((float)Mathf.Cos(180 - angle),
                                            (float)Mathf.Sin(180 - angle));
            weaponController = new WeaponController(this, power, direction, spawnPos);
        }

        public ScriptableObjWeapon ReturnWeaponScriptable()
        {
            return scriptableObjWeapon;
        }
    }
}