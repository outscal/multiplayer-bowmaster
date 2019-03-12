using System.Collections;
using System.Collections.Generic;
using Zenject;
using MultiplayerSystem;
using PlayerSystem;
using UnityEngine;

namespace InputSystem
{
    public class MouseInput : IInputComponent
    {
        private IInputService inputService;
        private IMultiplayerService multiplayerService;

        private Vector2 startMousePosition;
        private Vector2 endMousePosition;

        private float power;
        private float angle;
        private float maxDragDistance = Screen.width * 15 / 100;

        public MouseInput(IInputService inputService, IMultiplayerService multiplayerService)
        {
            this.inputService = inputService;
            this.multiplayerService = multiplayerService;
        }

        public void OnTick()
        {
            if(Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
                endMousePosition = Input.mousePosition;
              //  CalculateParameters(startMousePosition, endMousePosition);
            }
            if(Input.GetMouseButton(0))
            {
                startMousePosition = Input.mousePosition;
                CalculateParameters(startMousePosition, endMousePosition);
            }
            if(Input.GetMouseButtonUp(0))
            {
                endMousePosition = Input.mousePosition;
                CalculateParameters(startMousePosition, endMousePosition);

            }
        }

        //calculate angle and current distance
        private void CalculateParameters(Vector2 startPos, Vector2 endPos)
        {
            float currentDistance = Vector2.Distance(startPos, endPos) / Screen.width;
            if (currentDistance > maxDragDistance)
            {
                currentDistance = 10f;
            }
            power = currentDistance;
            float tangent = (endPos.y - startPos.y) / (endPos.x - startPos.x);
            angle = Mathf.Atan(tangent);
        }

    }

}