using MultiplayerSystem;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Zenject;

namespace InputSystem
{
    public class MouseInput : IInputComponent
    {
        private IInputService inputService;
        private IMultiplayerService multiplayerService;
        private IGameService gameService;

        private Vector2 startMousePosition;
        private Vector2 endMousePosition;

        private float power;
        private float angle;
        private float maxDragDistance = Screen.width * 0.1f;

        private int characterID;
        private string localPlayerID;

        public MouseInput(IInputService inputService, IMultiplayerService multiplayerService,IGameService gameService)
        {
            this.inputService = inputService;
            this.multiplayerService = multiplayerService;
            this.gameService = gameService;
            localPlayerID = inputService.GetLocalPlayerID();
        }

        public void OnTick()
        {
            //if(gameService.GetGameState()!=GameStateEnum.GAME_PLAY)
            //{
            //    return;
            //}
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
                endMousePosition = Input.mousePosition;
                if (inputService.CheckForCharacterPresence(startMousePosition))
                {
                    characterID = inputService.GetSelectedCharacterID();
                }
                else
                {
                    return;
                }
                InputData inputData = CreateInputData();
                inputService.SendPlayerData(inputData,true);
            }
            if (Input.GetMouseButton(0))
            {
                endMousePosition = Input.mousePosition;
                InputData inputData = CreateInputData();
                inputService.SendPlayerData(inputData,true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                endMousePosition = Input.mousePosition;
                InputData inputData= CreateInputData();
                multiplayerService.SendNewInput(inputData);
                //inputService.SendPlayerData(inputData,false);
            }
        }

        private InputData CreateInputData()
        {
            CalculateParameters(startMousePosition, endMousePosition);
            InputData inputData = new InputData();
            inputData.angleValue = angle;
            inputData.powerValue = power;
            inputData.characterID = characterID;
            inputData.playerID = inputService.GetLocalPlayerID(); 
            //multiplayerService.SendNewInput(inputData);
            return inputData;
        }

        //calculate angle and current distance
        private void CalculateParameters(Vector2 startPos, Vector2 endPos)
        {                  
            Vector2 vectorA = new Vector2(endPos.x-startPos.x,endPos.y-startPos.y);
            Vector2 vectorB = new Vector2(endPos.x-startPos.x,0);
            float currentDistance = Vector2.SqrMagnitude(vectorA);
            currentDistance=Mathf.Sqrt(currentDistance);
            power = currentDistance;
            
            if(power>maxDragDistance)
            {
                power = 100f;
            }
            angle = Vector2.Angle(vectorA, vectorB);
        }

    }

}