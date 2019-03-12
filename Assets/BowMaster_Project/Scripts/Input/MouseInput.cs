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
        private float maxDragDistance = Screen.width * 0.1f;

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

            }
        }

        //calculate angle and current distance
        private void CalculateParameters(Vector2 startPos, Vector2 endPos)
        {                  
            Vector2 vectorA = new Vector2(endPos.x-startPos.x,endPos.y-startPos.y);
            Vector2 vectorB = new Vector2(endPos.x-startPos.x,0);
            float currentDistance = Vector2.SqrMagnitude(vectorA);
            currentDistance=Mathf.Sqrt(currentDistance);
            power = currentDistance;
            Debug.Log("MAGNITUDE :" +power);
            if(power>maxDragDistance)
            {
                power = 100f;
            }
            angle = Vector2.Angle(vectorA, vectorB);
        }

    }

}