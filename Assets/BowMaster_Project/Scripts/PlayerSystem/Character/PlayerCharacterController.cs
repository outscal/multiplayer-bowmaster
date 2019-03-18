using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace PlayerSystem
{
    public class PlayerCharacterController
    {
        protected int characterID;
        protected PlayerController playerController;
        protected PlayerCharacterView playerCharacterView;
        protected ScriptableObjCharacter scriptableObjPlayer;
        protected IWeaponService weaponService;
        protected PlayerCharacterType playerCharacterType;
        protected WeaponType weaponType;
        protected PlayerService playerService;
        protected string localPlayerID;

        public int GetCharacterID()
        {
            return characterID;
        }

        public string GetLocalPlayerID()
        {
            return localPlayerID; 
        }

        public virtual void SetShootInfo(float power, float angle, bool gettingInput)
        {
            //Debug.Log("[CharacterController]WeaponType:" + scriptableObjPlayer.weaponType);
        }

        public void DeactivateInfoPanel()
        {
            playerCharacterView.DeactivateDisplayHolder();
        }

        public void SetHealthBarFirst(float health)
        {
            playerCharacterView.SetHealthBarLimits(0, health);
        }

        public void SetHealth(float health)
        {
            playerCharacterView.SetBarHealth(health); 
        }

        public virtual void SendDamageInfoToServer(float damage)
        {
            playerController.SendDamageInfoToServer(characterID,damage);
        }

        public void DestroyCharacter()
        {
            GameObject.Destroy(playerCharacterView.gameObject);
        }
    }
}