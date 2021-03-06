using UnityEngine;

/*
 * AudioManager
 * To control Audio throughout all scenes depending on the Audio Options from the MainMenu.
 */
namespace Main
{
    public class AudioManager : MonoBehaviour
    {
        public static void MuteActive()
        {
            if (AudioMenu.mute)
                AudioListener.volume = 0f;
            else if (!AudioMenu.mute)
                AudioListener.volume = 1f;
            else
                Debug.LogWarning("No audio in scene.");
        }
    }
}