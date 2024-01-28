using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace DefaultNamespace
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("Destroying duplicate Audio Manager", gameObject);
                Destroy(gameObject);
            }
        }


        public void PlayOneShot(string audioEvent)
        {
            RuntimeManager.PlayOneShot(audioEvent);
        }

        public void Play3DOneShot(string audioEvent, Vector3 position)
        {
            RuntimeManager.PlayOneShot(audioEvent, position);
        }

        public void StopOneShot(string audioEvent)
        {
        }

        public EventInstance CreateEventInstance(string audioEvent)
        {
            return RuntimeManager.CreateInstance(audioEvent);
        }
        
        public EventInstance CreateEventInstance(EventReference reference)
        {
            return RuntimeManager.CreateInstance(reference);
        }

        public EventInstance Create3DEventInstance(string audioEvent, GameObject gameObject)
        {
            try
            {
                var instance = RuntimeManager.CreateInstance(audioEvent);
                instance.set3DAttributes(gameObject.transform.To3DAttributes());
                return instance;
            }
            catch (Exception e)
            {
                return default;
            }
        }
        
        public void Play3DOneShot(EventInstance sound, GameObject gameObject)
        {
            sound.set3DAttributes(gameObject.transform.To3DAttributes());
            sound.start();
        }

        public void Play3DOneShot(EventReference reference, GameObject gameObject)
        {
            RuntimeManager.PlayOneShot(reference, gameObject.transform.position);

        }

        public void PlayOneShot(EventReference soundEvent)
        {
            RuntimeManager.PlayOneShot(soundEvent);
        }
    }
}