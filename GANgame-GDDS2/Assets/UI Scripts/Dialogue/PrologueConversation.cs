using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    [System.Serializable]
    public struct PrologueLine
    {
        public Character character;
        public bool transition;

        [TextArea(2, 5)]
        public string text;
    }

    [CreateAssetMenu(fileName = "New PrologueConversation", menuName = "PrologueConversation")]
    public class PrologueConversation : ScriptableObject
    {
        public Character speaker;
        public PrologueLine[] lines;
    }
}