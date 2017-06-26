using System.Collections;
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

        public float MaxDistance
        {
            get
            {
                List<float> _maxDistances = new List<float>();

                foreach (Sound _sound in Sounds)
                {
                    _maxDistances.Add(_sound.MaxDistance);
                }

                return _maxDistances.Max();
            }
        }

        public string ColliderTag;

        public void Start()
        {
            SphereCollider _sphere = gameObject.AddComponent<SphereCollider>();
            _sphere.isTrigger = true;
            _sphere.radius = MaxDistance;
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == ColliderTag)
            {
                foreach (Sound _sound in Sounds)
                {
                    _sound.Play();
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.tag == ColliderTag)
            {
                foreach (Sound _sound in Sounds)
                {
                    _sound.Stop();
                }
            }
        }
    }
}

