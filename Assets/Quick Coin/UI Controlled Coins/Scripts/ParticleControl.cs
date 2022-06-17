using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleControl : MonoBehaviour {

    [Header(" Events ")]
    public static UnityAction OnCoinsReleasedAction;
    public static UnityAction OnCoinAddedAction;

    public delegate void OnCoinAdded();
    public OnCoinAdded onCoinAdded;

    public delegate void OnControlledParticlesEnded();
    public OnControlledParticlesEnded onControlledParticlesEnded;
    
    [Header(" Settings  ")]
    public float particleSpeed = 3000;
    public float speedIncrement;
    float speed;
    public AudioSource releaseCoinsSound;
    public AudioSource coinSound;
    bool fountainSoundPlayed;
    float timer;
    public float pTime;
    float t = 0;
    public float waitBeforeMove = 1;

    private void Start()
    {

    }

    public void PlayControlledParticles(Vector2 pos, RectTransform targetUI, int particleCount = 10)
    {
        speed = particleSpeed * Screen.width / 1080f;
        ParticleSystem ps = GetComponent<ParticleSystem>();

        
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0, (short)particleCount, (short)particleCount);
        ps.emission.SetBurst(0, burst);

        transform.position = pos;
        StartCoroutine(PlayCoinParticlesCoroutine(ps, targetUI, particleCount));
    }

    IEnumerator PlayCoinParticlesCoroutine(ParticleSystem ps, RectTransform targetUIElement, int particleCount)
    {
        //yield return new WaitForSeconds(1);

        //speed = particleSpeed;
        fountainSoundPlayed = false;

        Vector3[] distances = new Vector3[particleCount];
        bool[] reached = new bool[particleCount];



        if(releaseCoinsSound != null)
        {
            releaseCoinsSound.Play();
            OnCoinsReleasedAction?.Invoke();
        }

        ps.Play();

        yield return new WaitForSeconds(waitBeforeMove);

        // Store the particles positions
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        for (int i = 0; i < distances.Length; i++)
            distances[i] = particles[i].position;
        


        while (ps.isPlaying)
        {
            particles = new ParticleSystem.Particle[ps.particleCount];

            ps.GetParticles(particles);
            

            for (int i = 0; i < particles.Length; i++)
            {
                Vector3 targetPos = Vector3.zero;
                
                
                targetPos.x = targetUIElement.position.x;
                targetPos.y = targetUIElement.position.y;
                targetPos.z = 0;
                

                Vector2 dir = targetPos - particles[i].position;    
                t += Time.deltaTime/2f;

                float smooth = Vector2.Distance(targetPos, distances[i]) / pTime;

                particles[i].position = Vector2.MoveTowards(particles[i].position, targetPos, smooth * Time.deltaTime);
                //particles[i].position = (Vector2)(targetPos - distances[i]) * pTime/t + (Vector2)distances[i];

                
                
                speed += speedIncrement;

                if (dir.magnitude < 0.05f)
                {
                    particles[i].color = new Color32(0, 0, 0, 0);
                    
                    if(!reached[i])
                    {
                        onCoinAdded?.Invoke();
                        OnCoinAddedAction?.Invoke();

                        reached[i] = true;

                        JetSystems.UIManager.AddCoins(1);
                    }
                   

                    if(coinSound != null && !fountainSoundPlayed)
                    {
                        coinSound.Play();
                        fountainSoundPlayed = true;
                    }
                }
            }

            ps.SetParticles(particles, particles.Length);

            timer += Time.deltaTime/2f;

            if(timer > 0.5f)
            {
                ps.Stop();           

                yield return null;
            }

            yield return new WaitForSeconds(Time.deltaTime / 2);
        }


        onControlledParticlesEnded?.Invoke();


        timer = 0;


        yield return null;
    }

    public bool IsPlaying()
    {
        return GetComponent<ParticleSystem>().isPlaying;
    }
}
