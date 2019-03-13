using UnityEngine;
using TMPro;

namespace PlayerSystem
{
    public class PlayerCharacterView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private GameObject displayHolder, shootPos;
        [SerializeField] private TextMeshProUGUI powerText, angleText;

        public Vector2 ShootPos { get { return shootPos.transform.position; } }
        private PlayerCharacterController playerCharacterController;

        public void SetCharacterController(PlayerCharacterController playerCharacterController)
        {
            this.playerCharacterController = playerCharacterController; 
        }

        public int GetCharacterID()
        {
            return playerCharacterController.GetCharacterID();
        }

        public void SetShootInfo(float power, float angle, bool gettingInput)
        {
            if (gettingInput == true)
            {
                powerText.text = power + " \n Power";
                angleText.text = angle + " \n Angle";
                displayHolder.SetActive(true);
            }
            else
            {
                displayHolder.SetActive(false); 
            }
        }

        public Vector2 GetForwardDirection()
        {
            return this.transform.right;
        }
    }
}