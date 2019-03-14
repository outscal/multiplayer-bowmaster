using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerScriptableObj", menuName = "Custom Objects/Player/PlayerObject", order = 0)]
    public class ScriptableObjCharacter : ScriptableObject
    {
        public string playerName;
        public PlayerCharacterType playerType;
        public PlayerCharacterView playerView;
        public int playerHealth;
        public WeaponType weaponType;
    }
}