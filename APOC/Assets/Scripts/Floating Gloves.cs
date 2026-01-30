using UnityEngine;
using System.Collections;

public class FloatingGloves2D : MonoBehaviour
{
    [Header("Glove References")]
    public Transform leftGlove;
    public Transform rightGlove;

    [Header("Floating")]
    public float floatSpeed = 2f;
    public float floatHeight = 0.15f;

    [Header("Punching")]
    public float punchDistance = 0.4f;
    public float punchSpeed = 20f;

    Vector3 startPos;
    Vector3 leftStart;
    Vector3 rightStart;
    bool punching;

    void Start()
    {
        startPos = transform.position;
        leftStart = leftGlove.localPosition;
        rightStart = rightGlove.localPosition;
    }

    void Update()
    {
        // Floating motion
        float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + Vector3.up * y;

        // TEST INPUT (you can change this later)
        if (Input.GetKeyDown(KeyCode.A))
            PunchLeft();

        if (Input.GetKeyDown(KeyCode.D))
            PunchRight();
    }

    public void PunchLeft()
    {
        if (!punching)
            StartCoroutine(PunchRoutine(leftGlove, leftStart, Vector3.left));
    }

    public void PunchRight()
    {
        if (!punching)
            StartCoroutine(PunchRoutine(rightGlove, rightStart, Vector3.right));
    }

    IEnumerator PunchRoutine(Transform glove, Vector3 startLocalPos, Vector3 dir)
    {
        punching = true;

        Vector3 target = startLocalPos + dir * punchDistance;

        // Punch out
        while (Vector3.Distance(glove.localPosition, target) > 0.01f)
        {
            glove.localPosition = Vector3.Lerp(
                glove.localPosition,
                target,
                Time.deltaTime * punchSpeed
            );
            yield return null;
        }

        // Return
        while (Vector3.Distance(glove.localPosition, startLocalPos) > 0.01f)
        {
            glove.localPosition = Vector3.Lerp(
                glove.localPosition,
                startLocalPos,
                Time.deltaTime * punchSpeed
            );
            yield return null;
        }

        glove.localPosition = startLocalPos;
        punching = false;
    }
}
