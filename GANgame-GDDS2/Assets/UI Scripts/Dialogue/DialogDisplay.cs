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
            
            if ( Input.touchCount > 0) //||Input.GetKeyDown(KeyCode.Space))
            {
                AdvanceConversation();
                EventManager.InteractEvent -= AdvanceConversation;
            }
            if (gameObject.activeInHierarchy == true)
            {
                pause.isPaused = true;
            }
        }



        public void AdvanceConversation()
        {
            Debug.Log("MOVING TEXT");
            if (activeLineIndex < conversation.lines.Length )
            {
                DisplayLines();
                activeLineIndex += 1;
            }
            else
            {
                speakerUI.dialog.text = null;
                pause.isPaused = false;
                activeLineIndex = 0;
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