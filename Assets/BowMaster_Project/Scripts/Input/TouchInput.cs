using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using PlayerSystem;
using GameSystem;
using MultiplayerSystem;
using System;

namespace InputSystem
{
   
    public class TouchInput :IInputComponent
    {
        private IInputService inputService;
        private IMultiplayerService multiplayerService;
        private IGameService gameService;
        
        private Vector2 startTouchPos;
        private Vector2 endTouchPos;
        private float maxDragDistance = Screen.width *15/100;
        private float angle;
        private float power;
        private int selectedID;
        private string localPlayerID;

        public TouchInput(IInputService inputService,IMultiplayerService multiplayerService,IGameService gameService)
        {
            this.inputService = inputService;
            this.multiplayerService = multiplayerService;
            this.gameService = gameService;
            localPlayerID=inputService.GetLocalPlayerID();
        }
      
        

        public void OnTick()
        {
            if (gameService.GetGameState() != GameStateEnum.GAME_PLAY)
            {
                return;
            }
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
                    inputData.localPlayerID = inputService.GetLocalPlayerID();
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
            Vector2 vectorA = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);
            Vector2 vectorB = new Vector2(endPos.x - startPos.x, 0);
            float currentDistance = Vector2.SqrMagnitude(vectorA);
            currentDistance = Mathf.Sqrt(currentDistance);
            power = currentDistance;
            Debug.Log("MAGNITUDE :" + power);
            if (power > maxDragDistance)
            {
                power = 100f;
            }
            angle = Vector2.Angle(vectorA, vectorB);
        }
    }
}