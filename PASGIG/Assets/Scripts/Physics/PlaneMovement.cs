using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlaneMovement : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float RotationControl;
    public Joystick joystick;

    Rigidbody2D rb;

    PhotonView view;

    public Animator afterburnerAnimator;
    private string currentState;

    //Animation states
    const string AFTERBURNER_IDLE = "idle";
    const string AFTERBURNER_P1 = "Power 1";
    const string AFTERBURNER_P2 = "Power 2";
    const string AFTERBURNER_P3 = "Power 3";


    //Joystick params
    float MovY, MovX = 1;
    Vector2 JoystickDir = Vector2.zero;

    //Wing behavior
    public float wingDrag = 1f; //this is for how much you slow down when you turn and how much of
    //this force gets converted into forward movement, like wingsize
    public float wingLift = 0.5f; //this for when you don't want your plane to crash (I'd rename it)
    //crashability but like that's ultimately unhelpful

    //Directional change vars
    bool rightFacing = true;
    float timeSinceDirChange = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            MovY = joystick.Vertical;
            MovX = joystick.Horizontal;
            JoystickDir = joystick.Direction;
        }
        
    }
    
    private void FixedUpdate()
    {


        float joystickMagnitude = (float) Math.Sqrt(MovX*MovX + MovY*MovY);

        //adding thrust based on magnitude of joystick displacement
        Vector2 Vel = transform.right * (joystickMagnitude * Acceleration);
        rb.AddForce(Vel);


        //changing animation state of afterburner
        if (joystickMagnitude == 0)
        {
            ChangeAnimationState(AFTERBURNER_IDLE);
        } else if (joystickMagnitude > 0 && joystickMagnitude < 0.33f)
        {
            ChangeAnimationState(AFTERBURNER_P1);
        }
        else if (joystickMagnitude >= 0.33f && joystickMagnitude < 0.66 )
        {
            ChangeAnimationState(AFTERBURNER_P2);
        }
        else
        {
            ChangeAnimationState(AFTERBURNER_P3);
        }

        //turning, speed based on rotation control
        if(Acceleration > 0)
        {
            //transform.right will be fixed as the forward direction
            float Dir = Vector2.SignedAngle(JoystickDir, transform.right);
            rb.rotation -= Dir * RotationControl;
        }
        

        //speed limit enforced using Speed
        if(rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }

        //adding wing drag
        Vector2 bleed = wingDrag * transform.up.normalized * Vector2.Dot(transform.up, rb.velocity);
        rb.AddForce(bleed * -1.0f);
        rb.AddForce(bleed.magnitude * transform.right);

        //adding nose droop like when control surfaces are disabled
        if(MovX == 0 && MovY == 0)
        {
            float Dir = Vector2.SignedAngle(rb.velocity, transform.right);
            rb.rotation -= Dir * RotationControl;
        }

        //adding wing lift
        //rb.AddForce(rb.velocity.x * transform.up * wingLift);
        
        float horizontalVelocity = Vector2.Dot(rb.velocity, Vector2.right);
        rb.AddForce(horizontalVelocity * Vector2.up * wingLift);
        rb.AddForce(horizontalVelocity * -Vector2.right * wingLift);
        

        //adding check to flip y scale of playerobject
        //
        if(rb.velocity.x < 0)
        {
            if (rightFacing)
            {
                rightFacing = false;
                Flip();
            }
        } else 
        {
            if (!rightFacing)
            {
                rightFacing = true;
                Flip();
            }
        }
    }
    private void Flip()
	{
        //TODO: trigger to activate animation
		transform.Rotate(180f, 0f, 0f);
	}

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        //play the animation
        afterburnerAnimator.Play(newState);
    }
}