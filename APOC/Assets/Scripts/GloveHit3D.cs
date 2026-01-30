
using UnityEngine;

public class GloveHit3D : MonoBehaviour
{
    public int damage = 25;
    public float knockbackForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ZombieHealth zombie = collision.gameObject.GetComponent<ZombieHealth>();
            Rigidbody zombieRb = collision.gameObject.GetComponent<Rigidbody>();

            if (zombie != null)
                zombie.TakeDamage(damage);

            if (zombieRb != null)
            {
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                zombieRb.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }

            Camera.main
                .GetComponent<SimpleCameraFollow>()
                .Shake(0.12f, 0.25f);
        }
    }
}
