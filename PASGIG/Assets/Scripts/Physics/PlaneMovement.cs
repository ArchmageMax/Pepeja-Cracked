using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using UnityEngine.UI;
using Photon.Pun;
>>>>>>> Stashed changes

public class PlaneMovement : MonoBehaviour
{

    //Speed Parameters
    public float Speed;
    public float Acceleration;
    public float RotationControl;

    //Joystick Controller
    public Joystick joystick;

    Rigidbody2D rb;

<<<<<<< Updated upstream
=======
    //PhotonView view;
>>>>>>> Stashed changes

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
    float joystickMagnitude = 0;

    //Wing behavior
    public float wingDrag = 1f; //this is for how much you slow down when you turn and how much of
    //this force gets converted into forward movement, like wingsize
    public float efficiency = 0.7f;

    //Directional change vars
    bool rightFacing = true;
    float timeSinceDirChange = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovY = joystick.Vertical;
        MovX = joystick.Horizontal;
        JoystickDir = joystick.Direction;
    }
    
    private void FixedUpdate()
    {
<<<<<<< Updated upstream


        joystickMagnitude = (float) Math.Sqrt(MovX*MovX + MovY*MovY);
        //changing animation state of afterburner
        UpdateAnimation();
        
=======
        float joystickMagnitude = (float) Math.Sqrt(MovX*MovX + MovY*MovY);
>>>>>>> Stashed changes

        //adding thrust based on magnitude of joystick displacement
        rb.AddForce(CalculateThrust());
        //adding wing drag
        //adding forward translated movement: depends on angle hitting wing
        rb.AddForce(CalculateWingPhysics());

        //turning, speed based on rotation control
        pitch();

        //speed limit enforced using Speed
        if(rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }

        //adding nose droop like when control surfaces are disabled
        if(MovX == 0 && MovY == 0)
        {
            float Dir = Vector2.SignedAngle(rb.velocity, transform.right);
            rb.rotation -= Dir * RotationControl;
        }

        //adding check to flip y scale of playerobject
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

    private Vector2 CalculateThrust() 
    {
        Vector2 thrust = transform.right * (joystickMagnitude * Acceleration);
        Debug.DrawLine(transform.position, (Vector2) transform.position + thrust, Color.yellow, 0.0f);
        return thrust;
    }

    void UpdateAnimation()
    {
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
    }

    void pitch()
    {
        if(Acceleration > 0)
        {
            //transform.right will be fixed as the forward direction
            float Dir = Vector2.SignedAngle(JoystickDir, transform.right);
            rb.rotation -= Dir * RotationControl;
        }
    }
    Vector2 CalculateWingPhysics()
    {
        //component perpendicular to wing
        Vector2 velocity = rb.velocity;
        Vector2 up = transform.up;
        float dot = Vector2.Dot(up, velocity);
        Vector2 drag = up * dot * -1.0f * wingDrag;
        Debug.DrawLine(transform.position, (Vector2) transform.position + drag, Color.green, 0.0f);

        //component parallel to wing
        float angleOfAttack = Vector2.Angle(transform.right, rb.velocity);
        Vector2 forwardMomentum = drag.magnitude * transform.right * efficiency * (1.0f - (angleOfAttack/90));
        Debug.DrawLine(transform.position, (Vector2) transform.position + forwardMomentum, Color.red, 0.0f);

        return drag + forwardMomentum;
    }
}