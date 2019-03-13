using UnityEngine;
using TMPro;

namespace PlayerSystem
{
    public class PlayerCharacterView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private GameObject displayHolder, shootPos;
        [SerializeField] private TextMeshProUGUI powerText, angleText;

        private PlayerCharacterController playerCharacterController;

        public void SetCharacterController(PlayerCharacterController playerCharacterController)
        {
            this.playerCharacterController = playerCharacterController; 
        }

        public int GetCharacterID()
        {
            return playerCharacterController.GetCharacterID();
        }

        public void SetShootInfo(float power, float angle)
        {
            powerText.text = power + " /nPower";
            angleText.text = angle + " /nAngle"; 
        }
    }
}