
using UnityEngine;
using System.Collections;

public class CharacterMotion : MonoBehaviour
{
    // Movement Physics
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    // Animation
    Animator anim;

    // Controller
    CharacterController controller;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        Movement();
        Rotation();
        Animation();
    }

    private void InitVariables()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Movement() {
        
        if (controller.isGrounded)
        {
            anim.SetBool("Grounded", true);
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                anim.SetTrigger("JumpTrigger");
                anim.SetBool("Grounded", false);
            }

            anim.SetBool("Moving", moveDirection.x != 0 || moveDirection.z != 0);
        }
        else
        { // Trajectory adjustment in mid air, like Mario...
            moveDirection.x = Input.GetAxis("Horizontal") * speed;
            moveDirection.z = Input.GetAxis("Vertical") * speed;
            moveDirection.y -= gravity * Time.deltaTime;

        }
        controller.Move(moveDirection * Time.deltaTime);


    }

    private void Rotation()
    {
        float rotateToAngle = (Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg);

        //transform.rotation = Quaternion.Euler(new Vector3(35, 0, 35));
        if (moveDirection.x != 0 || moveDirection.z != 0) {
            //transform.rotation = Quaternion.Euler(new Vector3(35, 0, 35));
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(new Vector3 (moveDirection.x, 0, moveDirection.z)), 0.10F);
        }
    }

    private void Animation() {
    }
}
