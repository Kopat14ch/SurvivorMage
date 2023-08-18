using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Particles.Scripts.Pool
{
    public class ParticlePool : MonoBehaviour
    {
        [SerializeField] private List<Particle> _prefabs;
        [SerializeField] private int _startCapacity;
        [SerializeField] private ParticleContainer _prefabContainer;

        private List<ParticleContainer> _containers;

        public void Awake()
        {
            _containers = new List<ParticleContainer>();

            foreach (Particle prefab in _prefabs)
            {
                ParticleContainer container = Instantiate(_prefabContainer, transform.position, Quaternion.identity,
                    transform);
                container.Init(prefab.ParticleType, prefab);
                _containers.Add(container);
                
                for (int i = 0; i < _startCapacity; i++)
                {
                    Particle spawned = Instantiate(prefab, transform.position, Quaternion.identity, container.transform);
                    spawned.Disable();
                    container.AddParticle(spawned);
                }
            }
        }

        public Particle GetObject(ParticleType particleType)
        {
            Particle particle = null;

            foreach (ParticleContainer container in _containers)
            {
                if (container.ParticleType == particleType)
                {
                    particle = container.GetParticle();
                    break;
                }
            }

            return particle;
        }
    }
}
