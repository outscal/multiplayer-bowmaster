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
        private IGameService gameService;
        
        private Vector2 startTouchPos;
        private Vector2 endTouchPos;
        private Vector2 forwardPosition;

        private float angle;
        private float power;
        private int selectedID;
        private string localPlayerID;
        private InputStatus inputStatus = InputStatus.INVALID;

        public TouchInput(IInputService inputService,IGameService gameService)
        {
            this.inputService = inputService;            
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
                     inputStatus = InputStatus.VALID;
                    selectedID = inputService.GetSelectedCharacterID();
                    forwardPosition = inputService.GetCharacterForwardDirection();
                }
                else
                {
                    return;
                }
                if (touch.phase == TouchPhase.Began && inputStatus == InputStatus.VALID)
                {
                    startTouchPos = touch.position;
                    endTouchPos = touch.position;
                    InputData inputData=CreateInputData();
                    inputService.SendPlayerData(inputData, true);
                }
                if(touch.phase==TouchPhase.Moved && inputStatus == InputStatus.VALID)
                {
                    endTouchPos = touch.position;
                    InputData inputData = CreateInputData();
                    inputService.SendPlayerData(inputData, true);
                }
                if (touch.phase == TouchPhase.Ended && inputStatus==InputStatus.VALID)
                {
                    endTouchPos = touch.position;
                    InputData inputData=  CreateInputData();
                    inputService.SendPlayerDataToServer(inputData);
                    inputStatus = InputStatus.INVALID;

                }
                
            }
        }

        private InputData CreateInputData()
        {
            CalculateAngleAndPower(startTouchPos, endTouchPos);
            InputData inputData = new InputData();
            inputData.angleValue = angle;
            inputData.powerValue = power;
            inputData.playerID = inputService.GetLocalPlayerID();
            inputData.characterID = selectedID;
            return inputData;          
        }

        //calculate angle and current distance
        private void CalculateAngleAndPower(Vector2 startPos, Vector2 endPos)
        {
            Vector2 vectorA = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);         
            float currentDistance = Vector2.SqrMagnitude(vectorA);
            power = Mathf.Sqrt(currentDistance);                       
            if (power > 100)
            {
                power = 100f;
            }
            angle = Vector2.SignedAngle(vectorA, forwardPosition);
            angle = angle >= 0 ? 180 - angle : -(180 + angle);

        }


    }
}