using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AudioToolkit
{
    public class SoundEmitter : MonoBehaviour
    {
        public Sound[] Sounds
        {
            get
            {
                if (sounds == null)
                {
                    sounds = gameObject.GetComponents<Sound>();
                }
                return sounds;
            }
        }

        Sound[] sounds;

        public AudioListener Listener
        {
            get
            {
                if (listener == null)
                    listener = FindObjectOfType<AudioListener>();

                return listener;
            }
        }

        AudioListener listener;

        public Vector3 ListenerPosition
        {
            get
            {
                return Listener.gameObject.transform.position;
            }
        }          

        public void Play(string soundName)
        {
            Sound _sound = GetSound(soundName);
            _sound.Play();
        }

        public void Stop(string soundName)
        {
            Sound _sound = GetSound(soundName);
            _sound.Stop();
        }

        Sound GetSound(string soundName)
        {
            foreach (Sound _sound in Sounds)
            {
                if (_sound.Name == soundName)
                {
                    return _sound;
                }
            }
            Debug.LogError("AudioToolkit :: No sound with name => " + soundName);
            return null;
        }
    }

}
