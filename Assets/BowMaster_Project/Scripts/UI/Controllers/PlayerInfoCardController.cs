using UnityEngine;
using TMPro;
using System.Collections;

namespace UISystem
{
    public class PlayerInfoCardController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameOfPlayer;

        public void SetPlayerName(string name)
        {
            nameOfPlayer.text = name;
        }
    }
}