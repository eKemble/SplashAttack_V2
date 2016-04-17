using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Collider))]

public class Player : MonoBehaviour {
    /*
    PROPERTIES OF A PLAYER:
    Dryness=Health=Float
    Water Supply=Ammo=Float
    WaterWeapon=Weapon Class
        */
    public float moveSpeed = 8f; //Multiplier that influences the Horizontal input
    public float jumpHeight = 6f; //increment if using just pure translational ascension
    public float jumpingForce = 3000; //Accelerated jump
    public float maxWaterCapacity = 100f; //Amount of water that user can carry
    public float currentWaterSupply; //Current amount of water 
    public WaterWeapon myWeapon; //Starts off at Waterballoon
    public Collider myCollider;
    public Rigidbody myRigidbody;
    public bool running; //Helps discern whether we use parabolic jump or normal jump. Probably can help with projectiles too
    public bool stall = false; //Bad word choice, but this helps set-up instant stop
    public int bursts = 10; //Movement Modifiers(Dash Left & Right, Quick Fall, Super Jump, wall jump)
    public bool iced = false; // If we do snowballs/icegun
    public bool _________________________;

    public GameObject go;
    public bool canJump = true;
    public Vector3 myMovement = new Vector3(0f, 0f, 0f);
    public bool inTheAir = false;
    // Use this for initialization


    void Start() {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        go = gameObject;
        Physics.gravity = new Vector3(0, -13F, 0); //I like this because you fall a little faster
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed; //Figures out if you are moving left or right, then scales it
        float airHorizontal = horizontal * 1.2f;//helps create a parabola affect when combined with the smaller jump height
        float parabolaJump = jumpingForce * .7f;
        float controllerHorizontal = Input.GetAxis("ControllerHorizontalLeftStick") * 6000;
        //var controllerVertical = Input.GetAxis("RightV") * 45;

        print(controllerHorizontal);
        //---------------------------------JUMP--------------------------------

        //If you press up and you are against a surface and you aren't holding the stall-button
        if ((Input.GetButtonDown("Vertical") || Input.GetKeyDown("joystick button 1")) && canJump == true && !stall)
        {
            //NORMAL JUMP
            if (!running)
            {
                //TRANSLATIONAL MOVEMENT transform.position += new Vector3(0, jumpHeight); 
                myMovement.y = jumpingForce;
                myRigidbody.AddForce(myMovement); //add force vector
                canJump = false;
                inTheAir = true;
            }
            if (running)//RUNNING JUMP not as high, but you go further
            {
                //TRANSLATIONAL MOVEMENT transform.position += new Vector3(airHorizontal*Time.deltaTime, parabolaJump, 0);
                myMovement.y = parabolaJump;
                myMovement.x = airHorizontal;
                myRigidbody.AddForce(myMovement);
                canJump = false; //you can't jump
                inTheAir = true; //air movement is affected
            }
        }

        if (Input.GetKeyDown("down") && inTheAir && bursts != 0) //ANTI-JUMP
        {
            myMovement.y -= jumpingForce; //Fall with the force you typically jump
            myRigidbody.AddForce(myMovement);
            bursts--;
        }

        if (Input.GetKeyDown("right shift") /*CONTROLLER*/ && inTheAir && bursts != 0)//HOVER
        {
        }


        //-----------------------------------------------------------------------------------------

        //----------------------------------------------MOVING-----------------------------------
        if ((Input.GetButton("Horizontal") && !inTheAir && !stall))//NORMAL MOVEMENT
        {
            transform.position += new Vector3(horizontal * Time.deltaTime, 0, 0);
            running = true;
                    }

        if (Input.GetButton("Horizontal") /*CONTROLLER*/&& inTheAir && !stall) //SLOWED DOWN LATERAL MOVEMENT
            transform.position += new Vector3(airHorizontal * Time.deltaTime, 0 , 0);

        if (horizontal == 0) //you aren't moving
            running = false;

        /*
        if ((Input.GetButton("ControllerHorizontal2") && !inTheAir && !stall))//NORMAL MOVEMENT
        {
           
            running = true;
            transform.position += new Vector3(controllerHorizontal * Time.deltaTime, 0, 0);
            print(controllerHorizontal);
            print("jaksjdfkaj;lsdfjaklsd");
        }*/
        //CONTROLLER LEFT STICK MOVEMENT
        if(Input.GetButton("ControllerHorizontalLeftStick") && !inTheAir && !stall)
        {
            transform.position += new Vector3(controllerHorizontal* Time.deltaTime, 0, 0);
            running = true;
            print("HI");
        }
        //---------------------------------------------------------------------------------------

        //-------------------------STALL MOVEMENT MODIFIERS----------------------------------------------

        if (Input.GetKey("right shift")/*CONTROLLER*/ && inTheAir != true) //Holding down
        {
            stall = true; //Stops player in their track

            if (Input.GetButtonDown("Vertical") && canJump == true && bursts != 0)//BIG JUMP
            {
                myMovement.y = jumpingForce*1.5f; 
                myRigidbody.AddForce(myMovement);
                canJump = false;
                inTheAir = true;
                myMovement = Vector3.zero;
                bursts--;
            }

            if (Input.GetKeyDown("left") /*CONTROLLER*/&& bursts != 0) //Burst Left
            {
                myMovement.x = -jumpingForce;
                myRigidbody.AddForce(myMovement);
                myMovement = Vector3.zero;
                bursts--;
            }

            if (Input.GetKeyDown("right") /*CONTROLLER*/ && bursts != 0)//Burst Right
            {
                myMovement.x = jumpingForce;
                myRigidbody.AddForce(myMovement);
                myMovement = Vector3.zero;
                bursts--;
            }

        }


            if (Input.GetKeyUp("right shift")/*CONTROLLER*/)
            stall = false;

        
    }
       
    void OnCollisionEnter(Collision collision)
    {
        //If you collide with a surface, you are now capable of moving again 
        if (collision.gameObject.tag == "Surface")
        {
            canJump = true;
            inTheAir = false;
            stall= false;
        }


        }

    void OnCollisionStay(Collision c)
    {
        canJump = true;
        inTheAir = false;
    }

    void OnCollisionExit(Collision c)
    {
        inTheAir = true;
        canJump = false;
    }
    
}
