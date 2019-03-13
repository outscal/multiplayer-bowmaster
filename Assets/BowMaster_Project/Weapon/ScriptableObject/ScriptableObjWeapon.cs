using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponScriptableObj", menuName = "Custom Objects/Weapon", order = 0)]
    public class ScriptableObjWeapon : ScriptableObject
    {
        public List<WeaponView> weaponList;
    }
}