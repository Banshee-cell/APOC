using UnityEngine;

public class ZombieChase3D : MonoBehaviour
{
    public float speed = 3f;
    Transform target;
    Rigidbody rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        if (!target) return;

        Vector3 direction = (target.position - transform.position);
        direction.y = 0; // keep zombie grounded
        direction.Normalize();

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // Face the gloves
        if (direction != Vector3.zero)
            transform.forward = direction;
    }
}
