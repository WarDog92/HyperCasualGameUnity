using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
    public Joystick joystick;
    [SerializeField]
    private CharacterController controller;

    private Vector3 moveDirection;
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

    public Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    void Update(){
        Vector2 direction = joystick.direction;

        if (direction == Vector2.zero) anim.Play("stay");

        if (controller.isGrounded){
            moveDirection = new Vector3(direction.x, 0, direction.y);
/*            anim.PlayQueued("stay", QueueMode.CompleteOthers);*/

            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
			transform.rotation = targetRotation;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * speed * Time.deltaTime);

        dash();
        dodge();

    }
    void dash()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentDashTime = 0.0f;
            speed = 0f;
            dashVector = new Vector3(moveDirection.x, 0, moveDirection.z);
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
        }
    }

    void dodge()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentDodgeTime = 0.0f;
            speed = 0f;
            dodgeVector = new Vector3(moveDirection.x, 0, moveDirection.z);
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
        }
    }

}
