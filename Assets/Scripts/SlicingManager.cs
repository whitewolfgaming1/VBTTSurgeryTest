using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingManager : MonoBehaviour
{
    public List<GameObject> objectsToSlice;
    public Transform slicingPosition;
    private int currentIndex = 0;

    void Start()
    {
        if (objectsToSlice.Count > 0)
            MoveObjectToSlicingPosition(objectsToSlice[0]);
    }

    public void OnObjectSliced(GameObject obj)
    {
        // Move sliced object out of the way is handled by SliceAndMove
        currentIndex++;
        if (currentIndex < objectsToSlice.Count)
            MoveObjectToSlicingPosition(objectsToSlice[currentIndex]);
    }

    private void MoveObjectToSlicingPosition(GameObject obj)
    {
        obj.transform.position = slicingPosition.position;
        obj.SetActive(true);
    }
}