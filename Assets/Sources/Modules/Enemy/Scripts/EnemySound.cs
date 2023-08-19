using Sources.Modules.Sound.Scripts;
using UnityEngine;

namespace Sources.Modules.Enemy
{
    public class EnemySound : MonoBehaviour
    {
        [SerializeField] private AudioClip _dieClip;
        [SerializeField] private AudioClip _attackClip;

        private AudioSource _audioSource;

        public void Init(SoundContainer container, AudioSource audioSource)
        {
            transform.parent = container.transform;
            _audioSource = Instantiate(audioSource, container.transform.position, Quaternion.identity, container.transform);
        }

        public void PlayDie(Vector3 position)
        {
            _audioSource.transform.position = position;

            _audioSource.clip = _dieClip;
            _audioSource.Play();
        }
        
    }
}