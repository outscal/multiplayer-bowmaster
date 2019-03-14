using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerScriptableObjList", menuName = "Custom Objects/Player/PlayerList", order = 0)]
    public class ScriptableObjCharacterList : ScriptableObject
    {
        public List<ScriptableObjCharacter> playerObject;
    }
}