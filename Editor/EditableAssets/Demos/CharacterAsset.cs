using UnityEngine;

namespace MuffinDev.Core.Demos.EditableAsset
{

    ///<summary>
    /// Asset used for the EditableAsset editor demos.
    /// Represents a generic character with name, description and RPG-like values.
    ///</summary>
    [CreateAssetMenu(fileName = "NewCharacterAsset", menuName = "Muffin Dev/Demos/Character Asset")]
    public class CharacterAsset : ScriptableObject
    {

        [SerializeField]
        private string characterName = string.Empty;
        public string Name { get { return !string.IsNullOrEmpty(characterName) ? characterName : name; } }

        [SerializeField, TextArea]
        private string description = string.Empty;
        public string Description => description;

        [SerializeField]
        private int maxHP = 100;
        public int MaxHP => maxHP;

        [SerializeField]
        private int force = 20;
        public int Force => force;

        [SerializeField]
        private int defense = 5;
        public int Defense => defense;

    }

}