using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WeaponSystem
{
    public class WeaponController : IWeaponController
    {
        protected WeaponService weaponService;
        protected WeaponView weaponView;
        protected WeaponType weaponType;
        protected bool LocalPlayer;

        protected virtual void SetWeaponType()
        {
            weaponType = WeaponType.Air;
        }

        protected WeaponView GetWeaponView()
        {
            for (int i = 0; i < weaponService.ReturnWeaponScriptable().weaponList.Count; i++)
            {
                if (weaponType == weaponService.ReturnWeaponScriptable().weaponList[i].weaponType)
                    return weaponService.ReturnWeaponScriptable().weaponList[i].weaponView;
            }

            return null;
        }

        public void DestroyWeapon()
        {
            WeaponInfo weaponInfo;
            weaponInfo.isLocalPlayer = LocalPlayer;
            weaponInfo.weaponController = this;
            weaponService.GetSignalBus().TryFire(new SignalDestroyWeapon() { weaponInfo = weaponInfo });
        }

        public GameObject GetWeaponGameObject()
        {
            return weaponView.gameObject;
        }

        public bool isLocalPlayer()
        {
            return LocalPlayer;
        }
    }
}