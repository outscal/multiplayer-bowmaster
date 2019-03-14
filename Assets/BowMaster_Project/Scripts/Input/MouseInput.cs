using GameSystem;
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
        private IGameService gameService;

        private Vector2 startMousePosition;
        private Vector2 endMousePosition;
        private Vector2 forwardPosition;

        private float power;
        private float angle;


        private int characterID;
        private string localPlayerID;

        private InputStatus inputStatus=InputStatus.INVALID;

        public MouseInput(IInputService inputService,  IGameService gameService)
        {
            this.inputService = inputService;            
            this.gameService = gameService;
            localPlayerID = inputService.GetLocalPlayerID();
        }

        public void OnTick()
        {
            //if (gameService.GetGameState() != GameStateEnum.GAME_PLAY)
            //{
            //    return;
            //}
            if (Input.GetMouseButtonDown(0) && inputService.CheckForCharacterPresence(Input.mousePosition))
            {
                inputStatus =InputStatus.VALID;
                startMousePosition = Input.mousePosition;
                endMousePosition = Input.mousePosition;
                characterID = inputService.GetSelectedCharacterID();
                forwardPosition = inputService.GetCharacterForwardDirection();
                InputData inputData = CreateInputData(startMousePosition, endMousePosition);
                inputService.SendPlayerData(inputData, true);

            }
            if (Input.GetMouseButton(0) && inputStatus == InputStatus.VALID)
            {
                endMousePosition = Input.mousePosition;
                InputData inputData = CreateInputData(startMousePosition, endMousePosition);
                inputService.SendPlayerData(inputData, true);
            }
            if (Input.GetMouseButtonUp(0) && inputStatus == InputStatus.VALID)
            {
                endMousePosition = Input.mousePosition;
                InputData inputData = CreateInputData(startMousePosition, endMousePosition);
                //inputService.SendPlayerDataToServer(inputData);
                inputService.SendPlayerData(inputData, false);
                inputStatus = InputStatus.INVALID;
               
            }

        }

        private InputData CreateInputData(Vector2 startPos, Vector2 endPos)
        {
            CalculateAngleAndPower(startPos, endPos);
            InputData inputData = new InputData();
            inputData.angleValue = angle;
            inputData.powerValue = power;
            inputData.characterID = characterID;
            inputData.playerID = inputService.GetLocalPlayerID();            
            return inputData;
        }

        //calculate angle and current distance
        private void CalculateAngleAndPower(Vector2 startPos, Vector2 endPos)
        {
            Vector2 vectorA = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);
            float currentDistance = Vector2.SqrMagnitude(vectorA);
            currentDistance = Mathf.Sqrt(currentDistance);
            power = currentDistance;

            if (power > 100)
            {
                power = 100f;
            }
            angle = Vector2.SignedAngle(vectorA, forwardPosition);
            angle = angle >= 0 ? 180 - angle : -(180 + angle);
        }

    }

}