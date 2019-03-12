using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using PlayerSystem;
using MultiplayerSystem;
using System;

namespace InputSystem
{
    public struct InputData
    {
        public float powerValue;
        public float angleValue;
        public int localPlayerID;
        public int characterID;

    }
    public class TouchInput :IInputComponent
    {
        private IInputService inputService;
        private IMultiplayerService multiplayerService;
        
        private Vector2 startTouchPos;
        private Vector2 endTouchPos;
        private float maxDragDistance = Screen.width *15/100;
        private float angle;
        private float power;
        private int selectedID;
        private int localPlayerID;

        public TouchInput(IInputService inputService,IMultiplayerService multiplayerService)
        {
            this.inputService = inputService;
            this.multiplayerService = multiplayerService;          
            localPlayerID=inputService.GetLocalPlayerID();
        }
      
        

        public void OnTick()
        {
            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);
                if(inputService.CheckForCharacterPresence(touch.position))
                {
                    selectedID = inputService.GetSelectedCharacterID();
                }

                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPos = touch.position;
                    endTouchPos = touch.position;
                    CalculateParameters(startTouchPos, endTouchPos);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPos = touch.position;
                    CalculateParameters(startTouchPos, endTouchPos);
                    InputData inputData = new InputData();
                    inputData.angleValue = angle;
                    inputData.powerValue = power;
                    inputData.localPlayerID = 
                    inputData.characterID =selectedID;
                    multiplayerService.SendNewInput(inputData);
                   
                }
                if(touch.position!=touch.position)
                {
                    CalculateParameters(startTouchPos, endTouchPos);
                }
            }
        }

        //calculate angle and current distance
        private void CalculateParameters(Vector2 startPos, Vector2 endPos)
        {
            float currentDistance=Vector2.Distance(startPos, endPos)/Screen.width;
            if(currentDistance>maxDragDistance)
            {
                currentDistance = 10f;
            }
            power = currentDistance;
            float tangent = (endPos.y - startPos.y) / (endPos.x-startPos.x);
            angle = Mathf.Atan(tangent);
        }
    }
}