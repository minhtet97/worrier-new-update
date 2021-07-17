using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController charController;
    private CharacterAnimation playerAnimations;

    public float movement_speed = 3f;
    public float gravity = 9.8f;
    public float rotation_Speed = 0.15f;
    public float rotateDegreesPerSecond = 180f;

    public bool alive;

    //1st function this is called

    void Start()
    {
        alive = true;  
    }
    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimations = GetComponent<CharacterAnimation>();

    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        AnimateWalk();
    }

    void Move()
    {

        if (Input.GetAxis("Vertical") > 0)
        {
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Vector3 moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_speed * Time.deltaTime);
        }
        else
        {
            charController.Move(Vector3.zero);
        }
    }//move.

    void Rotate()
    {
        Vector3 rotation_Direction = Vector3.zero;

        if (Input.GetAxis("Horizontal") < 0)
        {
            rotation_Direction = transform.TransformDirection(Vector3.left);
        }
        if (Input.GetAxis("Horizontal") >  0)
        {
            rotation_Direction = transform.TransformDirection(Vector3.right);
        }

        if (rotation_Direction != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, Quaternion.LookRotation(rotation_Direction),
                rotateDegreesPerSecond * Time.deltaTime);
        }
    }//rotate

    void AnimateWalk()
    {
        if (charController.velocity.sqrMagnitude != 0f)
        {
            playerAnimations.walk(true);
        }
        else
        {
            playerAnimations.walk(false);
        }
    }
}//class


