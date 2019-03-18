using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public struct WeaponInfo
    {
        public IWeaponController weaponController;
        public bool isLocalPlayer;
    }

    public class SignalDestroyWeapon
    {
        public WeaponInfo weaponInfo;
    }
}