using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    [System.Serializable]
    public struct PlayerViewInfo
    {
        public string playerName;
        public PlayerCharacterType playerType;
        public PlayerCharacterView playerView;
    }

    [CreateAssetMenu(fileName = "PlayerScriptableObj", menuName = "Custom Objects/Player", order = 0)]
    public class ScriptableObjPlayer : ScriptableObject
    {
        public List<PlayerViewInfo> characterViews;
    }
}