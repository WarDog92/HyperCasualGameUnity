using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Joystick joystick;
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private bool _isGrounded;

    public Vector3 moveVector;
    [SerializeField]
    public float speed = 0;

    private Vector3 dashVector;
    [SerializeField]
    private float dashSpeed = 100f;
    [SerializeField]
    private float maxDashTime = 1f;
    private float currentDashTime = 1f;
    [SerializeField]
    private float cooldownDash = 0;

    private Vector3 dodgeVector;
    [SerializeField]
    private float dodgeSpeed = 100f;
    [SerializeField]
    private float maxDodgeTime = 1f;
    private float currentDodgeTime = 1f;
    [SerializeField]
    private float cooldownDodge = 0;

    private Animator anim;

    private bool lockMove = false;
    private bool lockDash = false;
    private bool lockDodge = false;
    private bool lockRotate = false;

    [SerializeField]
    private float pushPower = 1.0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Check_kick();
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
        if (_isGrounded && !lockMove)
        {
            Vector2 direction = joystick.direction;
            Debug.Log("Lockfall");
            moveVector = new Vector3(direction.x, 0, direction.y);
            if (!lockRotate)
            {
                Quaternion targetRotation = moveVector != Vector3.zero ? Quaternion.LookRotation(moveVector) : transform.rotation;
                transform.rotation = targetRotation;
            }
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = new Vector3(moveVector.x * speed, rb.velocity.y, moveVector.z * speed);

        }
        else if (!_isGrounded )
        {
            anim.SetTrigger("fall");
            Debug.Log("fall");
        }

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
                lockMove = true;
                dashVector = new Vector3(moveVector.x, 0, moveVector.z);
                if (dashVector == Vector3.zero)
                {
                    dashVector = gameObject.transform.forward;
                }
            }
            if (currentDashTime < maxDashTime)
            {
                currentDashTime += Time.deltaTime;
                rb.velocity = dashVector * dashSpeed;
            }
            if (currentDashTime >= maxDashTime)
            {
                lockMove = false;
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
                gameObject.layer = 10;
                anim.SetTrigger("dodge");
                lockMove = true;
                dodgeVector = new Vector3(moveVector.x, 0, moveVector.z);
                if (dodgeVector == Vector3.zero)
                {
                    dodgeVector = gameObject.transform.forward;
                }
            }
            if (currentDodgeTime < maxDodgeTime)
            {
                currentDodgeTime += Time.deltaTime;
                rb.velocity = dodgeVector * dodgeSpeed;
            }
            else if (currentDodgeTime >= maxDodgeTime)
            {
                gameObject.layer = 0;
                lockMove = false;
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

    void Check_kick()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("beingKick") || anim.GetCurrentAnimatorStateInfo(0).IsName("fallAfterKick"))
        {
            lockMove = true;
            lockDash = true;
            lockDodge = true;
        }
        else
        {
            lockMove = false;
            lockDash = false;
            lockDodge = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            if (dashVector != Vector3.zero)
            {
                collision.rigidbody.AddForce(dashVector * pushPower * 10, ForceMode.Impulse);
                collision.gameObject.GetComponent<Animator>().SetTrigger("Kick");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }
    }

}