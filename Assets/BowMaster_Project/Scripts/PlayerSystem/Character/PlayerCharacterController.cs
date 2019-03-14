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

        public int GetCharacterID()
        {
            return characterID;
        }


        public virtual void SetShootInfo(float power, float angle, bool gettingInput)
        {
            playerCharacterView.SetShootInfo(power, angle,gettingInput);

            if (gettingInput == false)
                weaponService.SpawnWeapon(power, angle
                , playerCharacterView.ShootPos
                    , scriptableObjPlayer.weaponType);
        }

    }
}