using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMouseControl : MonoBehaviour
{
    private bool isRotating = false;
    private float initialRotationX;
    private float initialRotationZ;
    private Vector3 initialMousePosition;

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            isRotating = true;
            initialMousePosition = Input.mousePosition;
            initialRotationX = transform.rotation.eulerAngles.x;
            initialRotationZ = transform.rotation.eulerAngles.z;
        }
    }

    private void OnMouseUp()
    {
        isRotating = false;
    }

    private void Update()
    {
        if (isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - initialMousePosition;

            float rotationX = initialRotationX - mouseDelta.y;
            float rotationZ = initialRotationZ + mouseDelta.x;

            Quaternion rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, rotationZ);
            transform.rotation = rotation;
        }
    }
}
