using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public interface IWeaponService
    {
        void SpawnWeapon(float power, float angle, Vector2 position
        , WeaponType weaponType, bool localPlayer);
    }
}