using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AudioToolkit
{
    public class SoundEmitter2D : MonoBehaviour
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
            CircleCollider2D _circle = gameObject.AddComponent<CircleCollider2D>();
            _circle.isTrigger = true;
            _circle.radius = MaxDistance;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == ColliderTag)
            {
                foreach (Sound _sound in Sounds)
                {
                    _sound.Play();
                }
            }
        }

        void OnTriggerExit2D(Collider2D col)
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
