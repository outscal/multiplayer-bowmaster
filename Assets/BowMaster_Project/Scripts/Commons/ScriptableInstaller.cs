using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using PlayerSystem;
using WeaponSystem;
using UISystem;
using CameraSystem;

namespace Common
{
    [CreateAssetMenu(fileName = "Scriptable Settings", menuName = "Custom Objects/Installer/Scriptable Settings Attribute", order = 0)]
    public class ScriptableInstaller : ScriptableObjectInstaller
    {
        public ScriptableObjCharacterList scriptableObjPlayer;
        public ScriptableObjWeapon scriptableObjWeapon;
        public UIScriptableObj uIScriptableObj;
        public CameraScriptableObj cameraScriptableObj;

        public override void InstallBindings()
        {
            Container.BindInstances(scriptableObjPlayer);
            Container.BindInstances(scriptableObjWeapon);
            Container.BindInstances(uIScriptableObj);
            Container.BindInstances(cameraScriptableObj);
        }
    }
}