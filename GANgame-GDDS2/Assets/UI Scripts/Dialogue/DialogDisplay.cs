using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogDisplay : MonoBehaviour
    {
        public Conversation conversation;

        public GameObject speaker;
        private SpeakerUI speakerUI;

        private int activeLineIndex = 0;

        private void Start()
        {
            speakerUI = speaker.GetComponent<SpeakerUI>();
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
                DisplayLines();
                activeLineIndex += 1;
            }
            else
            {
                speakerUI.Hide();
                activeLineIndex = 0;
            }
        }

        void DisplayLines()
        {
            Line line = conversation.lines[activeLineIndex];
            Character character = line.character;

            if (speakerUI.SpeakerIs(character))
            {
                SetDialog(speakerUI, line.text);
            }
        }

        void SetDialog(SpeakerUI activeSpeakerUI, string text)
        {
            activeSpeakerUI.Dialog = text;
            activeSpeakerUI.Show();
        }
    }
}