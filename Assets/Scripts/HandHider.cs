using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHider : MonoBehaviour
{
    public Material handMaterial; // Assign in the Inspector

    public void HideHand()
    {
        if (handMaterial != null)
        {
            Color color = handMaterial.color;
            color.a = 0f; // Fully transparent
            handMaterial.color = color;
        }
    }

    public void ShowHand()
    {
        if (handMaterial != null)
        {
            Color color = handMaterial.color;
            color.a = 1f; // Fully opaque
            handMaterial.color = color;
        }
    }
}
