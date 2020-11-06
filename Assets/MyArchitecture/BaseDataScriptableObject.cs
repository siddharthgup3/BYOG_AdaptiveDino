using Sirenix.OdinInspector;
using UnityEngine;
#pragma warning disable 0649

namespace MyArchitecture
{
    public class BaseDataScriptableObject : SerializedScriptableObject
    {
        [TextArea] [FoldoutGroup("Description", true)]
        [SerializeField] private string description;
    }
    
    
}