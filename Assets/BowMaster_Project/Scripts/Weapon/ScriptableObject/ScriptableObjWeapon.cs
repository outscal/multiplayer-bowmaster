using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [System.Serializable]
    public struct WeaponViewInfo
    {
        public string playerName;
        public WeaponType weaponType;
        public WeaponView weaponView;
    }

    [CreateAssetMenu(fileName = "WeaponScriptableObj", menuName = "Custom Objects/Weapon", order = 0)]
    public class ScriptableObjWeapon : ScriptableObject
    {
        public List<WeaponViewInfo> weaponList;
    }
}