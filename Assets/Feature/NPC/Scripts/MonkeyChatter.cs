using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feature.NPC.Scripts
{
    public class MonkeyChatter : MonoBehaviour
    {
        private float _timeSinceLastAudio;
        private float _minTimeBetweenAudio = 5f;
        private float _maxTimeBetweenAudio = 30f;
        private float _timeToNextAudio;

        private void Update()
        {
            _timeSinceLastAudio += Time.deltaTime;
            if (_timeSinceLastAudio > _timeToNextAudio)
            {
                _timeSinceLastAudio = 0f;
                _timeToNextAudio = Random.Range(_minTimeBetweenAudio, _maxTimeBetweenAudio);
                AudioManager.instance.PlayOneShot("event:/Monkey Chatter");
            }
        }
    }
}