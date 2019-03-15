using UnityEngine;
using Zenject;
using PlayerSystem;
using System.Collections;

namespace CameraSystem
{
    public class CameraService : ICameraSystem
    {
        private IPlayerService playerService;
        private Vector3 turn1Pos;
        private Vector3 turn2Pos;
        private Camera mainCamera;

        public CameraService(IPlayerService playerService, CameraScriptableObj cameraScriptableObj)
        {
            this.playerService = playerService;
            mainCamera = cameraScriptableObj.mainCameraPrefab;
        }

        public void SetCameraPositions(Vector3 localPlayerPos, Vector3 remoteOpponentPos )
        {
            turn1Pos = localPlayerPos + new Vector3(0, 0, -5);
            turn2Pos=remoteOpponentPos+ new Vector3(0, 0, -5);
        }
        public void OnGameStart()
        {
            if(!playerService.IsCurrentPlayerTurn())
            {
                Vector3 tempPos = turn1Pos;
                turn1Pos = turn2Pos;
                turn2Pos = turn1Pos;               
            }
       //     mainCamera.transform.position = turn1Pos;
           iTween.MoveTo(mainCamera.gameObject, turn1Pos,0.2f);
        }

        public void SwitchCamera()
        {
            if(mainCamera.transform.position==turn1Pos)
            {
                //mainCamera.transform.position = turn2Pos;
                iTween.MoveTo(mainCamera.gameObject, turn2Pos,0.2f);
            }
            else
            {
                //mainCamera.transform.position = turn1Pos;
                iTween.MoveTo(mainCamera.gameObject, turn1Pos,0.2f);
            }
        }
    }
}