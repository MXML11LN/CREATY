using UnityEngine;

public class RotateCameraAroundPoint : MonoBehaviour
{
    public Transform targetPoint; // The point to rotate around
    public float rotationAngle = 30f; // The angle limit for rotation
    public float rotationSpeed = 20f; // Rotation speed

    private float currentAngle = 0f;
    private bool rotatingRight = true;
    private Vector3 initialOffset; // Offset from the target point

    void Start()
    {
        // Calculate the initial offset from the target point
        initialOffset = transform.position - targetPoint.position;
    }

    void Update()
    {
        // Calculate the rotation step based on speed and time
        float step = rotationSpeed * Time.deltaTime;

        // Rotate to the right
        if (rotatingRight)
        {
            currentAngle += step;

            if (currentAngle >= rotationAngle)
            {
                currentAngle = rotationAngle;
                rotatingRight = false; // Change direction
            }
        }
        // Rotate to the left
        else
        {
            currentAngle -= step;

            if (currentAngle <= -rotationAngle)
            {
                currentAngle = -rotationAngle;
                rotatingRight = true; // Change direction
            }
        }

        // Apply rotation around the target point
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedOffset = rotation * initialOffset;

        // Update the camera's position based on the rotated offset
        transform.position = targetPoint.position + rotatedOffset;

        // Keep the camera looking at the target point
        transform.LookAt(targetPoint);
    }
}