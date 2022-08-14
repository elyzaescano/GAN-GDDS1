using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Dialogue
{
    public class PrologueDialogDisplay : MonoBehaviour
    {
        public PrologueConversation conversation;
        public GameObject speaker;
        [SerializeField] bool transitionToggle;
        [SerializeField] bool canAdvance;
        public UnityEvent triggerTransition;

        SpeakerUI speakerUI;

        public float typeSpeed;
        int activeLineIndex = 0;

        [HideInInspector] public bool simulateClick = false;

        private void Start()
        {
            speakerUI = speaker.GetComponent<SpeakerUI>();
            speakerUI.Speaker = conversation.speaker;
            canAdvance = true;
        }

        private void Update()
        {   
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || simulateClick) 
            {
                if(isTyping) return;
                simulateClick = false;
                
                if(transitionToggle)
                {
                    canAdvance = false;
                    StartCoroutine(CutsceneTransition());
                }
                
                if(canAdvance)
                {
                    AdvanceConversation();
                }
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
                activeLineIndex = 0;
                speakerUI.dialog.text = null;
                conversation = null;
                gameObject.SetActive(false);
            }
        }

        void DisplayLines()
        {
            PrologueLine line = conversation.lines[activeLineIndex];
            Character character = line.character;
            transitionToggle = line.transition;
                        
            SetDialog(speakerUI, line.text, character, transitionToggle);
        }

        void SetDialog(SpeakerUI activeSpeakerUI, string text, Character speaker, bool ToggleDirector)
        {
            activeSpeakerUI.Show();
            activeSpeakerUI.Dialog = "";
            activeSpeakerUI.portrait.sprite = speaker.portrait;
            activeSpeakerUI.charName.text = speaker.charName;
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
        
        IEnumerator CutsceneTransition()
        {
            triggerTransition?.Invoke();
            yield return new WaitForSeconds(7);
            SceneManager.LoadScene("Tutorial");
        }
    }
}