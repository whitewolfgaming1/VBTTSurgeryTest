using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceAndMove : MonoBehaviour
{
public Material crossSectionMaterial;
    public Vector3 moveDirection = Vector3.right; // Direction to move halves (can set in Inspector)
    public float moveSpeed = 0.5f; // Units per second

    public SlicingManager slicingManager; // Assign in inspector or via script
    public bool IsSliced => isSliced; // Add this property
    
    private bool isSliced = false;
    private GameObject upperHullObj, lowerHullObj;

    void Update() {
        // Example: Press "S" to slice
        if (Input.GetKeyDown(KeyCode.S) && !isSliced) {
            SliceObject();
        }

        // If sliced, move the halves
        if (isSliced) {
            MoveHalves();
        }
    }

    public void SliceObject() {
        // Define the plane (example: world XZ plane at this object's position)
        EzySlice.Plane slicePlane = new EzySlice.Plane(transform.up, transform.position);

        SlicedHull slicedHull = gameObject.Slice(slicePlane, crossSectionMaterial);

        if (slicedHull != null) {
            // Create upper and lower hulls
            upperHullObj = slicedHull.CreateUpperHull(gameObject, crossSectionMaterial);
            lowerHullObj = slicedHull.CreateLowerHull(gameObject, crossSectionMaterial);

            // Optional: Set parent, layer, etc.
            upperHullObj.transform.position = transform.position;
            lowerHullObj.transform.position = transform.position;

            // Optionally add a script to each for movement (otherwise handle here)
            upperHullObj.AddComponent<MoveOutOfFrame>().Init(moveDirection, moveSpeed);
            lowerHullObj.AddComponent<MoveOutOfFrame>().Init(-moveDirection, moveSpeed);

            // Hide or destroy the original object
            gameObject.SetActive(false);

            isSliced = true;
        }
    }

    // If you want to handle movement here instead of on each hull, use this:
    void MoveHalves() {
        if (upperHullObj != null) {
            upperHullObj.transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        if (lowerHullObj != null) {
            lowerHullObj.transform.position -= moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}

// Helper script to add to hulls for independent movement
public class MoveOutOfFrame : MonoBehaviour {
    private Vector3 dir;
    private float speed;

    public void Init(Vector3 direction, float moveSpeed) {
        dir = direction.normalized;
        speed = moveSpeed;
    }

    void Update() {
        transform.position += dir * speed * Time.deltaTime;
        // Optionally: Destroy if far enough away
        if (Mathf.Abs(transform.position.x) > 50f || Mathf.Abs(transform.position.y) > 50f || Mathf.Abs(transform.position.z) > 50f) {
            Destroy(gameObject);
        }
    }
}
