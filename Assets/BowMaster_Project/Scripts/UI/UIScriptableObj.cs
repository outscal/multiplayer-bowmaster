using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "UI Settings", menuName = "Custom Objects/UI", order = 1)]
    public class UIScriptableObj : ScriptableObject
    {
        public UIView mainUICanvas;
        public GameObject playerCard;
    }
}