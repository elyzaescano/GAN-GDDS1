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

        private void Start()
        {
            speakerUI = speaker.GetComponent<SpeakerUI>();
            speakerUI.Speaker = conversation.speaker;

            pause = FindObjectOfType<PauseScreen>();

            AdvanceConversation();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AdvanceConversation();
            }
        }

        void AdvanceConversation()
        {
            if (activeLineIndex < conversation.lines.Length)
            {
                pause.isPaused = true;
                DisplayLines();
                activeLineIndex += 1;
            }
            else
            {
                pause.isPaused = false;
                activeLineIndex = 0;
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

        IEnumerator Typing(string text, SpeakerUI speaker)
        {
            foreach (char c in text.ToCharArray())
            {
                speaker.Dialog += c;
                yield return new WaitForSeconds(typeSpeed);
                
                if (GetComponent<AudioSource>())
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}