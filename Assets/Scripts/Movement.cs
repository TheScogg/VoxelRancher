using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private float moveHorizontal;
    private float moveVertical;
    private float jump;
    public bool canJump;
    public int rotSpeed = 50;
    public int speed = 100;
    public int jumpVelocity = 10;

    Vector3 dirVector;
    Vector3 moveTo;

    private bool isGrounded = true;

    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    float currentTime;
    float velocity;

    private Rigidbody rb;

    // Animator component & state info
    Animator anim;
    AnimatorStateInfo _stateInfo;

    // Ground detection to determine whether to jump
    RaycastHit hit;
    Ray rayToGround;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate() {
        isGrounded = Grounded();
        Move();
        Rotate();
        Animations();
        Debug.DrawRay(transform.position + Vector3.up, -Vector3.up);

    }

    bool Grounded() {
        return Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.1f);
    }

    void Animations()
    {
        if (isGrounded)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }
    }

    void Move() {
        dirVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveTo = transform.position + dirVector * speed * Time.deltaTime;

        if (Grounded())
            {

            rb.MovePosition(moveTo);
        }

        if (dirVector != Vector3.zero) {
            anim.SetBool("Moving", true);
        } else { anim.SetBool("Moving", false); }

        Jump();
    }

    void Jump()
    {
        if (Input.GetAxis("Jump") > 0 && CanJump()) {
            rb.AddForce(transform.up * jumpVelocity + dirVector * 200);
            anim.SetTrigger("JumpTrigger");
        }
    }

    private bool CanJump() {
        // Check to see if feet are on object, to allow jumping animation
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.1f))
        {
            if (isGrounded)
            {
                return true;

            }

        }
        return false;
    }

    //void OnAnimatorMove()
    //{
    //    _stateInfo = anim.GetCurrentAnimatorStateInfo(0);
    //    if (!_stateInfo.IsTag("Jump"))
    //    {

    //        anim.ApplyBuiltinRootMotion();
    //    }
    //}

    void Rotate() {
        // Smoothly tilts a transform towards a target rotation.
        float rotateToAngle = (Mathf.Atan2(dirVector.x, dirVector.z) * Mathf.Rad2Deg);
            currentTime += Time.deltaTime;

            // Dampen towards the target rotation
            if (dirVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotateToAngle, 0), .1f);

        }


    }

}

