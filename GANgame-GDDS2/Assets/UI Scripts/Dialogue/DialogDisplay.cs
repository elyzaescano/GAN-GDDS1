using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogDisplay : MonoBehaviour
    {
        public Conversation conversation;
        public GameObject speaker;

        SpeakerUI speakerUI;
        PauseScreen pause;

        public float typeSpeed;
        int activeLineIndex = 0;

        [HideInInspector] public bool simulateClick = false;

        private void Start()
        {
            speakerUI = speaker.GetComponent<SpeakerUI>();
            speakerUI.Speaker = conversation.speaker;

            pause = FindObjectOfType<PauseScreen>();

            simulateClick = true;
            simulateClick = false;
        }

        private void Update()
        {            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || simulateClick) 
            {
                if(isTyping) return;
            
                AdvanceConversation();
                EventManager.InteractEvent -= AdvanceConversation;
                simulateClick = false;
            } 
            if (gameObject.activeInHierarchy == true)
            {
                pause.isPaused = true; //pauses the game during dialog
            }
        }



        public void AdvanceConversation()
        {
            if (activeLineIndex < conversation.lines.Length )
            {
                DisplayLines();
                activeLineIndex += 1;
            }
            else
            {
                pause.isPaused = false;
                activeLineIndex = 0;
                speakerUI.dialog.text = null;
                conversation = null;
                //speakerUI.portrait.sprite = null;
                gameObject.SetActive(false);
            }
        }

        void DisplayLines()
        {
            Line line = conversation.lines[activeLineIndex];
            Character character = line.character;
                        
            SetDialog(speakerUI, line.text, character);
        }

        void SetDialog(SpeakerUI activeSpeakerUI, string text, Character speaker)
        {
            activeSpeakerUI.Show();
            activeSpeakerUI.Dialog = "";
            activeSpeakerUI.portrait.sprite = speaker.portrait;
            StopAllCoroutines();
            StartCoroutine(Typing(text, activeSpeakerUI));
        }

        bool isTyping = false;
        IEnumerator Typing(string text, SpeakerUI speaker)
        {
            isTyping = true;
            foreach (char c in text.ToCharArray())
            {
                speaker.Dialog += c;
                yield return new WaitForSeconds(typeSpeed);
                
                if (GetComponent<AudioSource>())
                {
                    GetComponent<AudioSource>().Play(); //plays the typing sound
                }
            }
            isTyping = false;
        }
    }
}