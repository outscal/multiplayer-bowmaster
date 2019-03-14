using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace PlayerSystem
{
    public class PlayerController : IPlayerController
    {
        private string playerID;

        private List<PlayerCharacterController> playerCharacterControllerList;
        private PlayerService playerService;
        private IWeaponService weaponService;
        private Vector2 spawnCharacterPos;

        public PlayerController(string playerID, PlayerService playerService
        , IWeaponService weaponSystem, Vector2 spawnPos)
        {
            this.playerService = playerService;
            this.weaponService = weaponSystem;
            this.playerID = playerID;
            spawnCharacterPos = spawnPos;
            playerCharacterControllerList = new List<PlayerCharacterController>();

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    PlayerCharacterController playerCharacterController = new CharacterAirController(
                            i, this, playerService.ReturnPlayerScriptableObj()
                            , weaponService, spawnCharacterPos
                        );
                    playerCharacterControllerList.Add(playerCharacterController);
                }
                else if (i == 1)
                {
                    PlayerCharacterController playerCharacterController = new CharacterWaterController(
                            i, this, playerService.ReturnPlayerScriptableObj()
                            , weaponService, spawnCharacterPos
                        );
                    playerCharacterControllerList.Add(playerCharacterController);
                }
                else if (i == 2)
                {
                    PlayerCharacterController playerCharacterController = new CharacterFireController(
                            i, this, playerService.ReturnPlayerScriptableObj()
                            , weaponService, spawnCharacterPos
                        );
                    playerCharacterControllerList.Add(playerCharacterController);
                }
                spawnCharacterPos.x += 2;
            }
        }

        public string ReturnPlayerID()
        {
            return playerID;
        }

        public void SetShootInfo(float power, float angle, int characterID, bool gettingInput)
        {
            playerCharacterControllerList[characterID].SetShootInfo(power, angle, gettingInput);
        }

        public void EndInput(int characterID)
        {
             
        }
    }
}