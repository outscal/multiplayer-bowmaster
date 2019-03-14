using UnityEngine;
using TMPro;
using WeaponSystem;

namespace PlayerSystem
{
    public class PlayerCharacterView : MonoBehaviour, IPlayerView
    {
        [SerializeField] protected GameObject displayHolder, shootPos;
        [SerializeField] protected TextMeshProUGUI powerText, angleText;

        public Vector2 ShootPos { get { return shootPos.transform.position; } }
        private PlayerCharacterController playerCharacterController;

        public virtual void SetCharacterController(PlayerCharacterController playerCharacterController)
        {
            this.playerCharacterController = playerCharacterController; 
        }

        public int GetCharacterID()
        {
            return playerCharacterController.GetCharacterID();
        }

        public virtual void SetShootInfo(float power, float angle, bool gettingInput)
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