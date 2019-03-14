using UnityEngine;
using System.Collections;
using TMPro;
using System;

namespace UISystem
{
    public class PopUpController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI popUptextComponent;

        private void Start()
        {
            if(popUptextComponent==null)
            {
                popUptextComponent = GetComponent<TextMeshProUGUI>();
            }
        }

        public  void SetText(string textString)
        {
            popUptextComponent.text = textString;
        }
    }
}