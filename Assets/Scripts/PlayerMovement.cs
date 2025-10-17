using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputAction playerControls;

    [Header("Magnet Settings")]
    [SerializeField] private float magnetRadius = 10f;
    [SerializeField] private float pullForce = 15f;
    [SerializeField] private string[] magneticTags = { "Ball", "BigBall" };
    [SerializeField] private InputAction magnetAction;

    [Header("Visual Feedback (Optional)")]
    [SerializeField] private bool showDebugRadius = true;

    Vector3 moveDirection = Vector3.zero;
    private List<Rigidbody> magneticObjects = new List<Rigidbody>();

    private void OnEnable()
    {
        playerControls.Enable();
        magnetAction.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        magnetAction.Disable();
    }

    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector3>();

        if (magnetAction.IsPressed())
        {
            ApplyMagnetEffect();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.z * moveSpeed);
    }

    private void ApplyMagnetEffect()
    {
        magneticObjects.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, magnetRadius);

        foreach (Collider col in colliders)
        {
            foreach (string tag in magneticTags)
            {
                if (col.CompareTag(tag) && col.attachedRigidbody != null)
                {
                    magneticObjects.Add(col.attachedRigidbody);
                    break;
                }
            }
        }

        foreach (Rigidbody magneticRb in magneticObjects)
        {
            Vector3 direction = (transform.position - magneticRb.position).normalized;
            float distance = Vector3.Distance(transform.position, magneticRb.position);

            float forceMagnitude = pullForce * (1 - distance / magnetRadius);
            magneticRb.AddForce(direction * forceMagnitude, ForceMode.Force);
        }
    }

    private void OnDrawGizmos()
    {
        if (showDebugRadius)
        {
            Gizmos.color = magnetAction != null && magnetAction.IsPressed()
                ? Color.cyan
                : new Color(0, 1, 1, 0.3f);
            Gizmos.DrawWireSphere(transform.position, magnetRadius);
        }
    }
}