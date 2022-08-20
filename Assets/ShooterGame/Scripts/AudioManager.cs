using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{

    public enum SFXList
    {
        PlayerShoot,
        EnemyShoot,
        EnemyKilled,
        GameOver
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public AudioSource[] Sources;
        public AudioClip[] Clips;

        public float Volume;

        public void PlaySfx(int clipIndex)
        {
            foreach (AudioSource source in Sources)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(Clips[clipIndex]);
                    return;
                }
            }
        }

        public void SetVolume(int volume)
        {
            Volume = volume;
            foreach (AudioSource source in Sources)
            {
                source.volume = Volume;
            }
        }

    }
}
