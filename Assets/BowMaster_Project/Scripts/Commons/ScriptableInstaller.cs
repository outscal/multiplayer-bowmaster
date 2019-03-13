using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using PlayerSystem;
using WeaponSystem;

namespace Common
{
    [CreateAssetMenu(fileName = "Scriptable Settings", menuName = "Custom Objects/Installer/Scriptable Settings Attribute", order = 0)]
    public class ScriptableInstaller : ScriptableObjectInstaller
    {
        public ScriptableObjPlayer scriptableObjPlayer;
        public ScriptableObjWeapon scriptableObjWeapon;

        public override void InstallBindings()
        {
            Container.BindInstances(scriptableObjPlayer);
            Container.BindInstances(scriptableObjWeapon);
        }
    }
}