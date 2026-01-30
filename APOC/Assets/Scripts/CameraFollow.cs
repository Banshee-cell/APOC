using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -8);
    public float smoothSpeed = 5f;

    private Vector3 shakeOffset;
    private float shakeDuration;
    private float shakeStrength;

    void LateUpdate()
    {
        if (target == null) return;

        if (shakeDuration > 0)
        {
            shakeOffset = Random.insideUnitSphere * shakeStrength;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }

        Vector3 desiredPosition = target.position + offset + shakeOffset;
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

    public void Shake(float duration, float strength)
    {
        shakeDuration = duration;
        shakeStrength = strength;
    }
}
