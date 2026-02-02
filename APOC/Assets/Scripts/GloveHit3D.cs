using UnityEngine;

public class GloveHit3D : MonoBehaviour
{
    [Header("Combat")]
    public int damage = 25;
    public float knockbackForce = 5f;

    [Header("Audio")]
    public AudioClip hitSound;   // single hit sound

    private AudioSource audioSource;
    private bool hasHitThisSwing;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Call this at the start of an attack animation
    public void StartSwing()
    {
        hasHitThisSwing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitThisSwing) return;
        if (!collision.gameObject.CompareTag("Enemy")) return;

        hasHitThisSwing = true;

        ZombieHealth zombie = collision.gameObject.GetComponent<ZombieHealth>();
        Rigidbody zombieRb = collision.gameObject.GetComponent<Rigidbody>();

        if (zombie != null)
            zombie.TakeDamage(damage);

        if (zombieRb != null)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            zombieRb.AddForce(direction * knockbackForce, ForceMode.Impulse);
        }

        // 🔊 PLAY HIT SOUND
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (Camera.main != null)
        {
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            if (cam != null)
                cam.Shake(0.08f, 0.2f);
        }
    }
}
