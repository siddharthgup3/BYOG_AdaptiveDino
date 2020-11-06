using Sirenix.OdinInspector;
using UnityEngine;

public class Sound : SerializedMonoBehaviour
{
    public static AudioSource audSource;
    private void Awake()
    {
        audSource = GetComponent<AudioSource>();
    }
}
