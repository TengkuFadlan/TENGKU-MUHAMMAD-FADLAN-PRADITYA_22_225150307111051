using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public Rigidbody2D playerRB;
    
    Camera mainCamera;
    Vector2 playerMove;
    Vector2 mousePosition;
    Vector2 lookDirection;
    float lookAngle;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {

        playerMove.x = Input.GetAxisRaw("Horizontal");
        playerMove.y = Input.GetAxisRaw("Vertical");
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        lookDirection = mousePosition - playerRB.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;

        playerMove = playerMove.normalized * playerSpeed;

        playerRB.MovePosition(playerRB.position + playerMove * Time.fixedDeltaTime);
        playerRB.rotation = lookAngle;
    }
}