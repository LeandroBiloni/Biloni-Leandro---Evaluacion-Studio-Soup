using ServiceLocating;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.Audio
{
    public class AudioManager : Installer, IAudioService
    {
        [Header("References")]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        public override void Install()
        {
            if (ServiceLocator.Instance)
                ServiceLocator.Instance.RegisterService<IAudioService>(this);

            _audioMixer.GetFloat("MusicVolume", out float musicDB);
            Debug.Log("Music db: " + musicDB);
            Debug.Log("Music float: " + DBToFloat(musicDB));
            _musicSlider.value = (int)DBToFloat(musicDB) * _musicSlider.maxValue;
            _musicSlider.onValueChanged.AddListener(UpdateMusicVolume);

            _audioMixer.GetFloat("SFXVolume", out float sfxDB);
            _sfxSlider.value = (int)DBToFloat(sfxDB) * _sfxSlider.maxValue;
            _sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
        }

        public AudioManager GetAudioManager()
        {
            return this;
        }

        public void PlaySound(SoundData soundData, GameObject sourceObject)
        {
            if (!sourceObject.TryGetComponent<AudioSource>(out AudioSource audioSource))
                audioSource = sourceObject.AddComponent<AudioSource>();

            audioSource.outputAudioMixerGroup = soundData.mixerGroup;
            audioSource.clip = soundData.clip;

            audioSource.Play();
        }

        private void UpdateSFXVolume(float value)
        {
            _audioMixer.SetFloat("SFXVolume", FloatToDB(value / 100));
        }

        private void UpdateMusicVolume(float value)
        {
            _audioMixer.SetFloat("MusicVolume", FloatToDB(value / 100));
        }

        private  float FloatToDB(float value)
        {
            return 20f * Mathf.Log10(value);
        }

        private float DBToFloat(float db)
        {
            return Mathf.Pow(10, db / 20f);
        }
    }
}

