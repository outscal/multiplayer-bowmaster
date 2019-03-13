﻿using System.Collections;
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

        public PlayerController(string playerID, PlayerService playerService
        , IWeaponService weaponSystem)
        {
            this.playerService = playerService;
            this.weaponService = weaponSystem;
            this.playerID = playerID;
            playerCharacterControllerList = new List<PlayerCharacterController>();

            for (int i = 0; i < 3; i++)
            {
                PlayerCharacterController playerCharacterController = new PlayerCharacterController(
                        i, this, playerService.ReturnPlayerScriptableObj()
                        , weaponService
                    );
                playerCharacterControllerList.Add(playerCharacterController);
            }
        }

        public string ReturnPlayerID()
        {
            return playerID;
        }

        public void SetShootInfo(float power, float angle, int characterID)
        {
            playerCharacterControllerList[characterID].SetShootInfo(power, angle);
        }
    }
}