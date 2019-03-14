using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace PlayerSystem
{
    public class CharacterAirController : PlayerCharacterController
    {
        public CharacterAirController(int characterID, PlayerController playerController,
        ScriptableObjCharacter scriptableObjPlayer, IWeaponService weaponService,
            Vector2 spawnPos)
        {
            this.scriptableObjPlayer = scriptableObjPlayer;
            this.characterID = characterID;
            this.weaponService = weaponService;
            this.playerController = playerController;
            this.playerCharacterType = scriptableObjPlayer.playerType;
            GameObject playerObj = GameObject.Instantiate<GameObject>(
                        scriptableObjPlayer.playerView.gameObject
                );
            playerObj.transform.position = spawnPos;
            playerCharacterView = playerObj.GetComponent<CharacterAirView>();
            playerCharacterView.SetCharacterController(this);
        }

        public override void SetShootInfo(float power, float angle, bool gettingInput)
        {
            playerCharacterView.SetShootInfo(power, angle, gettingInput);

            if (gettingInput == false)
                weaponService.SpawnWeapon(power, angle
                , playerCharacterView.ShootPos
                    , scriptableObjPlayer.weaponType);
        }


    }
}