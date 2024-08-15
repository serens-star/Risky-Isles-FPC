using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    private bool isDragging = false;

    // Start is called before the first frame update
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void Update()
    {
        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartDragging();
        }

        // Check if the mouse button is released
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopDragging();
        }

        // Continue dragging if currently dragging
        if (isDragging)
        {
            OnMouseDrag();
        }
    }

    private void StartDragging()
    {
        mousePosition = Input.mousePosition - GetMousePos();
        isDragging = true;
    }

    private void StopDragging()
    {
        isDragging = false;
    }

    private void OnMouseDown()
    {
        StartDragging();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
}

    
