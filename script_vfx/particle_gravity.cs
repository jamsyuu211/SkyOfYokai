using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_gravity : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Transform target;

    private ParticleSystem.Particle[] particles;

    void Start()
    {
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void Update()
    {
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particleToTarget = target.position - particles[i].position;
            float attractStrength = 5.0f; // 이 값을 조정하여 중심점으로의 힘을 조절하세요.

            particles[i].velocity += particleToTarget.normalized * attractStrength * Time.deltaTime;
        }

        particleSystem.SetParticles(particles, numParticlesAlive);
    }
}
