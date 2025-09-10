using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to your scalpel GameObject.
/// It makes the scalpel follow the mouse position in 2D space,
/// and allows for interactions using Unity physics (colliders, triggers).
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MouseFollow2D : MonoBehaviour
{
    [Tooltip("Offset from the mouse position (if you want to offset the scalpel's tip).")]
    public Vector2 mouseOffset = Vector2.zero;

    [Tooltip("If true, the scalpel will only move while the left mouse button is held down.")]
    public bool onlyFollowWhenMouseDown = false;

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("MouseFollow2D requires a Collider2D for interactions!");
        }
    }

    void Update()
    {
        if (onlyFollowWhenMouseDown && !Input.GetMouseButton(0))
            return;

        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0; // Ensure z=0 for 2D

        transform.position = (Vector2)mouseWorld + mouseOffset;
    }

    // Example interaction with other objects:
    void OnTriggerEnter2D(Collider2D other)
    {
        // Example: print the name of the object the scalpel touches
        Debug.Log("Scalpel touched: " + other.name);

        // You can add custom logic here, like cutting or changing states
        // Example:
        // if (other.CompareTag("Cuttable"))
        // {
        //     other.GetComponent<CuttableObject>().Cut();
        // }
    }
}