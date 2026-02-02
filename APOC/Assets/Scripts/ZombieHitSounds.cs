using UnityEngine;

public class ZombieHitSounds : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    [Tooltip("3 common hit sounds")]
    public AudioClip[] commonHitSounds; // size = 3

    [Tooltip("Rare hit sound")]
    public AudioClip rareHitSound;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float rareChance = 0.1f; // 10% chance

    public void PlayHitSound()
    {
        if (audioSource == null) return;

        float roll = Random.value; // 0–1

        if (roll <= rareChance && rareHitSound != null)
        {
            audioSource.PlayOneShot(rareHitSound);
        }
        else if (commonHitSounds.Length > 0)
        {
            AudioClip clip = commonHitSounds[Random.Range(0, commonHitSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
