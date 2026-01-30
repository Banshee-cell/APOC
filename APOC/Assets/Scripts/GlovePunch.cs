using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlovePunch : MonoBehaviour
{
    public KeyCode punchKey = KeyCode.Q;
    public float punchDistance = 0.3f;
    public float punchSpeed = 15f;
    public int punchDamage = 25;
    public float punchRadius = 0.2f;

    public float flingForce = 5f; // How strongly zombies are flung

    private Vector3 startLocalPos;
    private bool isPunching;
    private HashSet<ZombieHealth> hitZombies;

    void Start()
    {
        startLocalPos = transform.localPosition;
        hitZombies = new HashSet<ZombieHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(punchKey) && !isPunching)
        {
            StartCoroutine(Punch());
        }
    }

    IEnumerator Punch()
    {
        isPunching = true;
        hitZombies.Clear();

        Vector3 targetPos = startLocalPos + Vector3.forward * punchDistance;

        // Move forward
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                targetPos,
                punchSpeed * Time.deltaTime
            );

            DealDamage();

            yield return null;
        }

        // Move back
        while (Vector3.Distance(transform.localPosition, startLocalPos) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                startLocalPos,
                punchSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.localPosition = startLocalPos;
        isPunching = false;
    }

    void DealDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, punchRadius);

        foreach (Collider col in hitColliders)
        {
            ZombieHealth zombie = col.GetComponent<ZombieHealth>();
            if (zombie != null && !hitZombies.Contains(zombie))
            {
                zombie.TakeDamage(punchDamage);
                hitZombies.Add(zombie);

                // Apply fling force
                Rigidbody rb = zombie.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 flingDirection = (col.transform.position - transform.position).normalized;
                    rb.AddForce(flingDirection * flingForce, ForceMode.Impulse);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, punchRadius);
    }
}
