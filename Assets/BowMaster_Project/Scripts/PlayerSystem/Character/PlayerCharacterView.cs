using UnityEngine;
using TMPro;
using WeaponSystem;
using UnityEngine.UI;
using Common;

namespace PlayerSystem
{
    public class PlayerCharacterView : MonoBehaviour, ICharacterView, ITakeDamage
    {
        [SerializeField] protected GameObject displayHolder, shootPos;
        [SerializeField] protected TextMeshProUGUI powerText, angleText;
        [SerializeField] protected Slider healthBar;

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
        }

        public void DeactivateDisplayHolder()
        {
            displayHolder.SetActive(false);
        }

        public Vector2 GetForwardDirection()
        {
            return this.transform.right;
        }

        public void SetHealthBarLimits(float min,float max)
        {
            healthBar.minValue = min;
            healthBar.maxValue = max;
            healthBar.value = max;
        }

        public void SetBarHealth(float value)
        {
            healthBar.value = value;
        }

        public virtual void DamageAmount(float value)
        {
            playerCharacterController.SendDamageInfoToServer(value);
        }
    }
}