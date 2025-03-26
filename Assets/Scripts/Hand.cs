using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;

    internal void SetGrip(float v) {
        gripTarget = v;
    }

    internal void SetTrigger(float v) {
        triggerTarget = v;
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility() {
        mesh.enabled = !mesh.enabled;
    }
}
