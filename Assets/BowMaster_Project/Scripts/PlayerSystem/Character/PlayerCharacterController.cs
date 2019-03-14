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
        protected ScriptableObjPlayer scriptableObjPlayer;
        protected IWeaponService weaponService;
        protected PlayerCharacterType playerCharacterType;

        public int GetCharacterID()
        {
            return characterID;
        }

        protected virtual void SetPlayerType()
        {
            playerCharacterType = PlayerCharacterType.Air; 
        }

        public virtual void SetShootInfo(float power, float angle, bool gettingInput)
        {
            playerCharacterView.SetShootInfo(power, angle,gettingInput);

            if (gettingInput == false)
                weaponService.SpawnWeapon(power, angle
                , playerCharacterView.ShootPos
                    , WeaponType.Air);
        }

        protected PlayerCharacterView ReturnPlayerView()
        {
            for (int i = 0; i < scriptableObjPlayer.characterViews.Count; i++)
            {
                if (playerCharacterType == scriptableObjPlayer.characterViews[i].playerType)
                    return scriptableObjPlayer.characterViews[i].playerView;
            }

            return null;
        }

    }
}