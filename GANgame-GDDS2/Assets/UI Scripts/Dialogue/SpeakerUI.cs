using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class SpeakerUI : MonoBehaviour
    {
        public Image portrait;
        public Text charName;
        public Text dialog;

        private Character speaker;

        public Character Speaker
        {
            get { return speaker; }
            set
            {
                speaker = value;
                portrait.sprite = speaker.portrait;
                charName.text = speaker.charName;
            }
        }
        public string Dialog
        {
            set { dialog.text = value; }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);

        }
    }
}
