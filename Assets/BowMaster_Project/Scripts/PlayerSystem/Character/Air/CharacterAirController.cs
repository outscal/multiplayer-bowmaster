using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace PlayerSystem
{
    public class CharacterAirController : PlayerCharacterController
    {
        public CharacterAirController(int characterID, PlayerController playerController,
        ScriptableObjPlayer scriptableObjPlayer, IWeaponService weaponService,
            Vector2 spawnPos)
        {
            SetPlayerType();
            this.scriptableObjPlayer = scriptableObjPlayer;
            this.characterID = characterID;
            this.weaponService = weaponService;
            this.playerController = playerController;

            GameObject playerObj = GameObject.Instantiate<GameObject>(
                        ReturnPlayerView().gameObject
                );
            playerObj.transform.position = spawnPos;
            playerCharacterView = playerObj.GetComponent<CharacterAirView>();
            playerCharacterView.SetCharacterController(this);
        }

        protected override void SetPlayerType()
        {
            playerCharacterType = PlayerCharacterType.Air;
        }

        public override void SetShootInfo(float power, float angle, bool gettingInput)
        {
            playerCharacterView.SetShootInfo(power, angle, gettingInput);

            if (gettingInput == false)
                weaponService.SpawnWeapon(power, angle
                , playerCharacterView.ShootPos
                    , WeaponType.Air);
        }


    }
}