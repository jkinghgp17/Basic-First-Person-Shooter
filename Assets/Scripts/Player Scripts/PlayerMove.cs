using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private CharacterController charControl;

    public float walkSpeed;
    public float jumpSpeed;
    public int jumpNumMax = 1; // Number of jumps 
    private int jumpNumCurrent;
    private float gravity = 9.8f;
    private float fallMultipler = 2.5f;
    private float lowJumpMultipler = 2.0f;
    private float vSpeed = 0; // Current Vectical Velocity

    public float knockBackAmount = 0f;
    public Vector3 knockBackDirection = Vector3.zero;

	void Awake () {
        charControl = GetComponent<CharacterController>();
        jumpNumCurrent = jumpNumMax;
    }
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
	}

    void MovePlayer()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 moveDirSide = transform.right * horiz;
        Vector3 moveDirForward = transform.forward * vert;

        Vector3 move = moveDirSide + moveDirForward;
        
        if (move.magnitude > 1)
        {
            move /= move.magnitude;
        }

        move *= walkSpeed;

        if (charControl.isGrounded)
        {
            vSpeed = 0;
            jumpNumCurrent = jumpNumMax;
        }

        if (Input.GetKeyDown("space") && jumpNumCurrent >= 0)
        {
            vSpeed += jumpSpeed;
            jumpNumCurrent--;
        }

        if (vSpeed <= 0)
        {
            vSpeed -= gravity * fallMultipler * Time.deltaTime;
        }
        else if (vSpeed > 0)
        {
            vSpeed -= gravity * lowJumpMultipler * Time.deltaTime;
        }

        move.y = vSpeed;

        move += knockBackDirection * knockBackAmount;

        charControl.Move(move * Time.deltaTime);
    }

    public void addSpeed(float amount)
    {
        walkSpeed += amount;
    }

    public void addJump(float amount)
    {
        jumpSpeed += amount;
    }

    public void addKnockBack(float amount, Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            knockBackDirection = direction;
            amount = 0f;
            return;
        }

        knockBackAmount = amount;
        knockBackDirection = direction;
    }
}
