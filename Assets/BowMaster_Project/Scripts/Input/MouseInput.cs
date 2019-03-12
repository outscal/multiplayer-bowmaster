using MultiplayerSystem;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
        private float maxDragDistance = Screen.width * 0.02f;

        private int characterID;
        private int localPlayerID;

        public MouseInput(IInputService inputService, IMultiplayerService multiplayerService)
        {
            this.inputService = inputService;
            this.multiplayerService = multiplayerService;
            localPlayerID = inputService.GetLocalPlayerID();
        }

        public void OnTick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
                endMousePosition = Input.mousePosition;
                //  CalculateParameters(startMousePosition, endMousePosition);
                if (inputService.CheckForCharacterPresence(startMousePosition))
                {
                    characterID = inputService.GetSelectedCharacterID();
                }
            }          
            if (Input.GetMouseButtonUp(0))
            {
                endMousePosition = Input.mousePosition;
                CalculateParameters(startMousePosition, endMousePosition);
                InputData inputData = new InputData();
                inputData.angleValue = angle;
                inputData.powerValue = power;
                inputData.characterID = characterID;
                inputData.localPlayerID = localPlayerID;

                inputService.SendPlayerData(inputData);
                Debug.Log("SENDING PLAYER DATA");

            }
        }

        //calculate angle and current distance
        private void CalculateParameters(Vector2 startPos, Vector2 endPos)
        {
            Debug.Log("MAX DRAG DISTANCE"+ maxDragDistance);
         
            float currentDistance = Vector2.Distance(startPos, endPos);
            //if (currentDistance >= maxDragDistance)
            //{
            //    currentDistance = 10f;
            //}
            power = currentDistance;

            float slope1 = (endPos.y - startPos.y) / (endPos.x - startPos.x);
            Debug.Log("Slope1 "+ slope1);
            
            float slope2 = (endPos.y) / (endPos.x - startPos.x);
            Debug.Log("Slope2 "+ slope2);

            float angleInRadian = (slope1-slope2) * Mathf.PI / 180;
            angle = Mathf.Atan(angleInRadian);
        }

    }

}