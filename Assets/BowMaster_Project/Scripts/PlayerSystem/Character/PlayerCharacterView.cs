using UnityEngine;
using TMPro;

namespace PlayerSystem
{
    public class PlayerCharacterView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private GameObject displayHolder;
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
    }
}