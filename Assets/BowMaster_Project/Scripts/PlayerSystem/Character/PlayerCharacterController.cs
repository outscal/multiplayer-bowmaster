using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerCharacterController
    {
        private int characterID;
        private PlayerController playerController;
        private PlayerCharacterView playerCharacterView;
        private ScriptableObjPlayer scriptableObjPlayer;

        public PlayerCharacterController(int characterID, PlayerController playerController,
        ScriptableObjPlayer scriptableObjPlayer)
        {
            this.scriptableObjPlayer = scriptableObjPlayer;
            this.characterID = characterID;
            this.playerController = playerController;
            GameObject playerObj = GameObject.Instantiate<GameObject>(
                        scriptableObjPlayer.characterViews[0].gameObject
                );
            playerCharacterView = playerObj.GetComponent<PlayerCharacterView>();
            playerCharacterView.SetCharacterController(this);
        }

        public int GetCharacterID()
        {
            return characterID;
        }
    }
}