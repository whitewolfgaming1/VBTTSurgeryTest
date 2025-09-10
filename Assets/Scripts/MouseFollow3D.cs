using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to your scalpel GameObject.
/// It makes the scalpel follow the mouse position in 3D space (using a camera-facing plane),
/// and allows for interactions using Unity physics (colliders, triggers).
/// </summary>
[RequireComponent(typeof(Collider))]
public class MouseFollow3D : MonoBehaviour
{
    [Tooltip("Set the distance from the camera where the scalpel will follow the mouse.")]
    public float planeDistance = 2f;

    [Tooltip("Offset from the mouse position in world space.")]
    public Vector3 positionOffset = Vector3.zero;

    [Tooltip("If true, the scalpel will only move while the left mouse button is held down.")]
    public bool onlyFollowWhenMouseDown = false;

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (GetComponent<Collider>() == null)
        {
            Debug.LogWarning("MouseFollow3D requires a Collider for interactions!");
        }
    }

    void Update()
    {
        if (onlyFollowWhenMouseDown && !Input.GetMouseButton(0))
            return;

        // Create a plane facing the camera at a certain distance
        Plane plane = new Plane(-mainCamera.transform.forward, mainCamera.transform.position + mainCamera.transform.forward * planeDistance);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        float enter = 0f;
        if (plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            transform.position = hitPoint + positionOffset;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (onlyFollowWhenMouseDown && !Input.GetMouseButton(0))
           return;

        if (other.CompareTag("Cuttable"))
        {
            var slicer = other.GetComponent<SliceAndMove>();
            if (slicer != null && !slicer.IsSliced)
            {
                slicer.SliceObject();
            }
        }
    }
}