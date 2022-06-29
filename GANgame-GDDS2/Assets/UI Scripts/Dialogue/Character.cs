using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class Character : ScriptableObject
    {
        public Sprite portrait;
        public string charName;
    }
}