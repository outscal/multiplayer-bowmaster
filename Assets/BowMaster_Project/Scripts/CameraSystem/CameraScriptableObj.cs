using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
    [CreateAssetMenu(fileName = "Camera Settings", menuName = "Custom Objects/Camera", order = 0)]
    public class CameraScriptableObj : ScriptableObject
    {
        public Camera mainCameraPrefab;
        
    }
}