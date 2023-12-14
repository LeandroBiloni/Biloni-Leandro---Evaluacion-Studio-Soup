using UnityEngine;
using UnityEngine.Audio;

namespace Game.Audio
{
    [CreateAssetMenu(fileName = "Sound Data", menuName = "Scriptable Objects/Sound Data")]
    public class SoundData : ScriptableObject
    {
        public AudioClip clip;
        public AudioMixerGroup mixerGroup;
    }
}

