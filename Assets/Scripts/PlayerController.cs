using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Joystick joystick;
    [SerializeField]
    private CharacterController controller;

    public Vector3 moveVector;
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float gravity = 0;

    private Vector3 dashVector;
    [SerializeField]
    private float dashSpeed = 100f;
    [SerializeField]
    private float maxDashTime = 1f;
    [SerializeField]
    private float currentDashTime = 1f;

    private Vector3 dodgeVector;
    [SerializeField]
    private float dodgeSpeed = 100f;
    [SerializeField]
    private float maxDodgeTime = 1f;
    [SerializeField]
    private float currentDodgeTime = 1f;

    private Animator anim;

    private bool lockDash = false;
    private bool lockDodge = false;
    private bool lockRotate = false;
    [SerializeField]
    private float cooldownDash = 0;
    [SerializeField]
    private float cooldownDodge = 0;
    [SerializeField]
    private float heightForFalling = -10;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
        move();
        dash();
        dodge();

    }
    private void move()
    {
        Vector2 direction = joystick.direction;
        if (controller.isGrounded)
        {
            moveVector = new Vector3(direction.x, 0, direction.y);
            if (!lockRotate)
            {
                Quaternion targetRotation = moveVector != Vector3.zero ? Quaternion.LookRotation(moveVector) : transform.rotation;
                transform.rotation = targetRotation;
            }
        }
        else if (!controller.isGrounded)
        {
            moveVector.y = moveVector.y - (gravity * Time.deltaTime);
            if (moveVector.y < heightForFalling) anim.SetTrigger("fall");
        }

        controller.Move(moveVector * speed * Time.deltaTime);
    }

    void dash()
    {
        if (!(currentDodgeTime < maxDodgeTime))
        {
            if (Input.GetKeyDown(KeyCode.F) && !lockDash)
            {
                lockDash = true;
                lockRotate = true;
                Invoke("Lock_dash", cooldownDash);
                currentDashTime = 0.0f;
                anim.SetTrigger("dash");
                speed = 0f;
                dashVector = new Vector3(moveVector.x, 0, moveVector.z);
                if (dashVector == Vector3.zero)
                {
                    dashVector = gameObject.transform.forward;
                }
            }
            if (currentDashTime < maxDashTime)
            {
                currentDashTime += Time.deltaTime;
                controller.Move(dashVector * dashSpeed * Time.deltaTime);
            }
            if (currentDashTime >= maxDashTime)
            {
                speed = 30f;
                lockRotate = false;
                dashVector = new Vector3(0, 0, 0);
            }
        }
    }

    void dodge()
    {
        if (!(currentDashTime < maxDashTime))
        {
            if (Input.GetKeyDown(KeyCode.G) && !lockDodge)
            {
                lockDodge = true;
                lockRotate = true;
                Invoke("Lock_dodge", cooldownDodge);
                currentDodgeTime = 0.0f;
                anim.SetTrigger("dodge");
                speed = 0f;
                dodgeVector = new Vector3(moveVector.x, 0, moveVector.z);
                if (dodgeVector == Vector3.zero)
                {
                    dodgeVector = gameObject.transform.forward;
                }
            }
            if (currentDodgeTime < maxDodgeTime)
            {
                currentDodgeTime += Time.deltaTime;
                controller.Move(dodgeVector * dodgeSpeed * Time.deltaTime);
            }
            else if (currentDodgeTime >= maxDodgeTime)
            {
                speed = 30f;
                lockRotate = false;
                dodgeVector = new Vector3(0, 0, 0);
            }
        }
    }
    void Lock_dash()
    {
        lockDash = false;
    }
    void Lock_dodge()
    {
        lockDodge = false;
    }
}