using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    [Header("Effect Settings")]
    public float duration = 2f;
    public bool playOnStart = true;
    public bool destroyOnComplete = true;
    [Header("Components")]
    public ParticleSystem[] particleSystems;
    public Animator animator;
    public AudioSource audioSource;

    private void Start()
    {
        if (playOnStart)
        {
            PlayEffect();
        }
        if (destroyOnComplete)
        {
            Destroy(gameObject, duration);
        }
    }
    private void PlayEffect()
    {
        if (particleSystems != null)
        {
            foreach (var ps in particleSystems)
            {
                if (ps != null)
                {
                    ps.Play();
                }
            }
        }
        if (animator != null)
        {
            animator.SetTrigger("Play");
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
    public void StopEffect()
    {
        if (particleSystems != null)
        {
            foreach (var ps in particleSystems)
            {
                if (ps != null)
                    ps.Stop();
            }
        }
    }
} 