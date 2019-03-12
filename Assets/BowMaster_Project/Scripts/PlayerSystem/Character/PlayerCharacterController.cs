using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerCharacterController
    {
        private int characterID;
        private PlayerController playerController;

        public PlayerCharacterController(int characterID, PlayerController playerController)
        {
            this.characterID = characterID;
            this.playerController = playerController;
        }

        public int GetCharacterID()
        {
            return characterID;
        }
    }
}