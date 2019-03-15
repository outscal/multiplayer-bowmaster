using UnityEngine;
using UnityEditor;

namespace PlayerSystem
{
    public struct HitInfo
    {
        public string playerId;
        public int characterId;
        public float characterHealth;
        public bool destroy;
    }
}