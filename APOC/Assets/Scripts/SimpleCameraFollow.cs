using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;   // Boxing gloves
    public float smoothSpeed = 10f;
    public Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        if (!target) return;

        // Follow gloves EXACTLY on X/Y, lock Z
        Vector3 newPos = new Vector3(
            target.position.x,
            target.position.y,
            offset.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            newPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
