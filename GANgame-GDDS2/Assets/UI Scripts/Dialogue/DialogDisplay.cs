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
            //AdvanceConversation();


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
            print("Advance conversation:");
            if (activeLineIndex < conversation.lines.Length )
            {
                print("Liens displayed");
                DisplayLines();
                activeLineIndex += 1;
            }
            else
            {
                print("lines not displayed");
                activeLineIndex = 0;
                speakerUI.dialog.text = null;
                pause.isPaused = false;
                conversation = null;
                gameObject.SetActive(false);
            }
        }

        void DisplayLines()
        {
            Line line = conversation.lines[activeLineIndex];
            Character character = line.character;

             SetDialog(speakerUI, line.text);
           
        }

        void SetDialog(SpeakerUI activeSpeakerUI, string text)
        {
            activeSpeakerUI.Show();
            activeSpeakerUI.Dialog = "";
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