using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using Zenject;

namespace CameraSystem
{
    public enum CameraTurn
    {
        TURN1,
        TURN2
    }
    public class CameraService : ICameraService
    {
        private IPlayerService playerService;
        private Vector3 turn1Pos;
        private Vector3 turn2Pos;
        private Camera mainCamera;
        private Vector3 offset=new Vector3(0,0,-5f);
        private GameObject weaponToFollow;
        private CameraTurn currentTurn;

        public CameraService(IPlayerService playerService, SignalBus signalBus)
        {
            this.playerService = playerService;            
            mainCamera = Camera.main;
            signalBus.Subscribe<SignalSpawnWeapon>(SetWeaponToFollow);
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
            //iTween.MoveTo(mainCamera.gameObject, turn2Pos, 0.2f);
            //await new WaitForSeconds(0.2f); 
            iTween.MoveTo(mainCamera.gameObject, turn1Pos, 0.2f);
            currentTurn = CameraTurn.TURN1;
            await new WaitForSeconds(0.2f);
        }
        async public void SwitchCamera()
        {
            if (currentTurn==CameraTurn.TURN1)
            {
                
                iTween.MoveTo(mainCamera.gameObject, turn2Pos, 0.2f);
                await new WaitForSeconds(0.2f);
                mainCamera.orthographicSize = 7f;
            }
            else
            {
                
                iTween.MoveTo(mainCamera.gameObject, turn1Pos, 0.2f);
                await new WaitForSeconds(0.2f);
                mainCamera.orthographicSize = 7f;
                currentTurn = CameraTurn.TURN2;
            }
        }
        public void ResetCameraOrthoSize()
        {
            mainCamera.orthographicSize = 15f;

        }

        async public void FollowProjectile()
        {
            while (weaponToFollow!=null)
            {
                mainCamera.transform.localPosition= new Vector3(weaponToFollow.transform.localPosition.x,
                                                                weaponToFollow.transform.localPosition.y,
                                                                mainCamera.transform.localPosition.z);
                await new WaitForEndOfFrame();
            }
        }

        private void SetWeaponToFollow(SignalSpawnWeapon spawnWeaponSignal)
        {
            weaponToFollow = spawnWeaponSignal.weaponObject;
        }
    }
}