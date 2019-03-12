using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerScriptableObj", menuName = "Custom Objects/Player", order = 0)]
    public class ScriptableObjPlayer : ScriptableObject
    {
        public List<PlayerCharacterView> characterViews;
    }
}