using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Rigidbody2D cameraTarget;
    public float cameraLerp = 1f;

    Camera mainCamera;
    Vector3 newCameraPosition;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        newCameraPosition = mainCamera.transform.position;
    }
    
    void FixedUpdate()
    {
        newCameraPosition.x = cameraTarget.transform.position.x;
        newCameraPosition.y = cameraTarget.transform.position.y;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCameraPosition, cameraLerp * Time.fixedDeltaTime);
    }
}