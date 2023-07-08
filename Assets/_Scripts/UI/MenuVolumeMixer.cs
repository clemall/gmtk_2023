using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.UI
{
    public class MenuVolumeMixer : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }
    }
}
