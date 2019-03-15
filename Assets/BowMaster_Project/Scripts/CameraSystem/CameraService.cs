using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CameraSystem
{
    public class CameraService : ICameraService
    {
        private IPlayerService playerService;
        private Vector3 turn1Pos;
        private Vector3 turn2Pos;
        private Camera mainCamera;
        private Vector3 offset=new Vector3(0,0,-5f);

        public CameraService(IPlayerService playerService, CameraScriptableObj cameraScriptableObj)
        {
            this.playerService = playerService;
            //mainCamera = cameraScriptableObj.mainCameraPrefab;
            mainCamera = Camera.main;
        }
        
        async public void OnGameStart()
        {
            List<Vector3> cameraPos=playerService.GetCameraPositions();
            turn1Pos = cameraPos[0] + offset;
            turn2Pos = cameraPos[1] + offset;

            if (!playerService.IsCurrentPlayerTurn())
            {
                Vector3 tempPos = turn1Pos;
                turn1Pos = turn2Pos;
                turn2Pos = turn1Pos;
            }
            
            mainCamera.orthographicSize = 7f;
            iTween.MoveTo(mainCamera.gameObject, turn2Pos, 0.2f);
            await new WaitForSeconds(0.2f);
            iTween.MoveTo(mainCamera.gameObject, turn1Pos, 0.2f);
            await new WaitForSeconds(0.2f);
        }
        async public void SwitchCamera()
        {
            if (mainCamera.transform.position == turn1Pos)
            {
                //mainCamera.transform.position = turn2Pos;
                iTween.MoveTo(mainCamera.gameObject, turn2Pos, 0.2f);
                await new WaitForSeconds(0.2f);
                mainCamera.orthographicSize = 7f;
            }
            else
            {
                //mainCamera.transform.position = turn1Pos;
                iTween.MoveTo(mainCamera.gameObject, turn1Pos, 0.2f);
                await new WaitForSeconds(0.2f);
                mainCamera.orthographicSize = 7f;
            }
        }
        public void ResetCameraOrthoSize()
        {
            mainCamera.orthographicSize = 15f;

        }
    }
}