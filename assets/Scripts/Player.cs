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
    public float jumpingForce = 500; //Accelerated jump
    public float maxWaterCapacity = 100f; //Amount of water that user can carry
    public float maxBalloons = 5;
    public float currentBalloons = 0;
    public float balloonVelocity = 5;
    public float currentWaterSupply; //Current amount of water 
    public int health=100;
    public GameObject balloonPrefab;
    public WaterWeapon myWeapon; //Starts off at Waterballoon
    public Collider myCollider;
    public Rigidbody myRigidbody;
    public bool running; //Helps discern whether we use parabolic jump or normal jump. Probably can help with projectiles too
    public bool stall = false; //Bad word choice, but this helps set-up instant stop
    public int bursts = 10; //Movement Modifiers(Dash Left & Right, Quick Fall, Super Jump, wall jump)
    public bool iced = false; // If we do snowballs/icegun
    public bool _________________________;
    public bool right = true;
    public bool projectile = true;
    public GameObject go;
    public bool canJump = true;
    public Vector3 myMovement = new Vector3(0f, 0f, 0f);
    public bool inTheAir = false;
    static public int playersNum;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject player3Prefab;
    public GameObject player4Prefab;
    float h;
    Vector3 dir;
    float vel;
    float dist;
    float a;
    // Use this for initialization


    void Start() {
        if(playersNum == 2)
        {
            GameObject player1 = Instantiate(player1Prefab) as GameObject;
            player1.transform.position= new Vector3(-6.5f, -4.8f, 0);
            GameObject player2 = Instantiate(player2Prefab) as GameObject;
            player2.transform.position = new Vector3(5.9f, -4.8f, 0);
        }
        if (playersNum == 3)
        {
            GameObject player1 = Instantiate(player1Prefab) as GameObject;
            player1.transform.position = new Vector3(-6.5f, -4.8f, 0);
            GameObject player2 = Instantiate(player2Prefab) as GameObject;
            player2.transform.position = new Vector3(5.9f, -4.8f, 0);
            GameObject player3 = Instantiate(player3Prefab) as GameObject;
            player3.transform.position = new Vector3(5.53f, -0.02f, 0);
        }
        if (playersNum == 4)
        {
            GameObject player1 = Instantiate(player1Prefab) as GameObject;
            player1.transform.position = new Vector3(-6.5f, -4.8f, 0);
            GameObject player2 = Instantiate(player2Prefab) as GameObject;
            player2.transform.position = new Vector3(5.9f, -4.8f, 0);
            GameObject player3 = Instantiate(player3Prefab) as GameObject;
            player3.transform.position = new Vector3(5.53f, -0.02f, 0);
            GameObject player4 = Instantiate(player4Prefab) as GameObject;
            player4.transform.position = new Vector3(-5.53f, -0.02f, 0);
        }

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
        float controllerHorizontal = Input.GetAxis("ControllerHorizontalLeftStick") * 50;
        //var controllerVertical = Input.GetAxis("RightV") * 45;

        // print(controllerHorizontal);
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
        if (((Input.GetButton("Horizontal")) && !inTheAir && !stall))//NORMAL MOVEMENT
        {
            transform.position += new Vector3(horizontal * Time.deltaTime, 0, 0);
            //print(horizontal);
            running = true;
            if(horizontal > 0)
            {
            right = true;
            }
            if(horizontal < 0)
            {
                right = false;
            }
            
                    }

        if (Input.GetButton("Horizontal")  /*CONTROLLER*/&& inTheAir && !stall)
        {//SLOWED DOWN LATERAL MOVEMENT
            transform.position += new Vector3(airHorizontal * Time.deltaTime, 0, 0);
            if (horizontal > 0)
            {
                right = true;
            }
            if (horizontal < 0)
            {
                right = false;
            }
        }
        if (horizontal == 0) //you aren't moving
            running = false;

        /*
        if ((Input.GetButton("ControllerHorizontalDpad") && !inTheAir && !stall))//NORMAL MOVEMENT
        {
            running = true;
            transform.position += new Vector3(controllerHorizontal * moveSpeed* Time.deltaTime, 0, 0);
            print(controllerHorizontal);
            print("jaksjdfkaj;lsdfjaklsd");
        }*/

        if(controllerHorizontal!=0 /*&& !inTheAir && !stall*/)
        {
            running = true;
            transform.position += new Vector3(controllerHorizontal*moveSpeed * Time.deltaTime, 0, 0);
            if (horizontal > 0)
            {
                right = true;
            }
            if (horizontal < 0)
            {
                right = false;
            }
            //print(controllerHorizontal);
            //print("jaksjdfkaj;lsdfjaklsd");
        }

       /* //CONTROLLER LEFT STICK MOVEMENT
        if(Input.GetButton("ControllerHorizontalLeftStick") && !inTheAir && !stall)
        {
            transform.position += new Vector3(controllerHorizontal* Time.deltaTime, 0, 0);
            running = true;
            print("HI");
        }*/
        //---------------------------------------------------------------------------------------

        //-------------------------STALL MOVEMENT MODIFIERS----------------------------------------------

        if ((Input.GetKey("right shift") || Input.GetButton("Stall"))/*CONTROLLER*/ && inTheAir != true) //Holding down
        {
            stall = true; //Stops player in their track

            if ((Input.GetButtonDown("Vertical") || Input.GetKeyDown("joystick button 1")) && canJump == true && bursts > 0)//BIG JUMP
            {
                myMovement.y = jumpingForce*1.5f; 
                myRigidbody.AddForce(myMovement);
                canJump = false;
                inTheAir = true;
                myMovement = Vector3.zero;
                bursts--;
            }

            if ((Input.GetKeyDown("left")) /*CONTROLLER*/&& bursts > 0) //Burst Left
            {
                myMovement.x = -jumpingForce;
                myRigidbody.AddForce(myMovement);
                myMovement = Vector3.zero;
                bursts--;
            }

            if ((Input.GetKeyDown("right")) /*CONTROLLER*/ && bursts > 0)//Burst Right
            {
                myMovement.x = jumpingForce;
                myRigidbody.AddForce(myMovement);
                myMovement = Vector3.zero;
                bursts--;
            }

            if(controllerHorizontal < 0)
            {
                myMovement.x = -jumpingForce/100;
                myRigidbody.AddForce(myMovement);
                myMovement = Vector3.zero;
                bursts--;
                controllerHorizontal = 0;
            }


            if (controllerHorizontal > 0)
            {
                myMovement.x = jumpingForce/100;
                myRigidbody.AddForce(myMovement);
                myMovement = Vector3.zero;
                bursts--;
                controllerHorizontal = 0;
            }
            

        }


            if (Input.GetKeyUp("right shift") || Input.GetButtonUp("Stall")/*CONTROLLER*/)
            stall = false;

            if (Input.GetButtonDown("Fire2") || Input.GetButtonDown("ControllerFire2"))
            {
            if (currentBalloons > 0)
            {

                GameObject balloon = Instantiate(balloonPrefab) as GameObject;
                Vector3 pos2 = this.transform.position;
                if (right == true) {
                    pos2.x += .5f;
                    pos2.y += .5f;
                    balloon.transform.position = pos2;
                    Vector3 pos = this.transform.position;
                    pos.x += 10;
                    pos.y = 0;
                    dir = pos - transform.position;
                    h = dir.y;
                    dir.y = 0;
                    dist = dir.magnitude;
                    //a = 90 * Mathf.Deg2Rad;
                    //dist += h / Mathf.Tan(a);
                    vel = Mathf.Sqrt(dist * Physics.gravity.magnitude );

                }
                else
                    {
                    pos2.x -= .5f;
                    pos2.y += .5f;
                    balloon.transform.position = pos2;
                    Vector3 pos = this.transform.position;
                    pos.x -= 10;
                    pos.y = 0;
                    dir = pos - transform.position;
                    h = dir.y;
                    dir.y = 0;
                    dist = dir.magnitude;
                    //a = 90 * Mathf.Deg2Rad;
                    //dist += h / Mathf.Tan(a);
                    vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
 
                }
                print(vel * dir.normalized);
                balloon.GetComponent<Rigidbody>().velocity = vel * dir.normalized;
                currentBalloons--;
            }
            }
        
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
        if (collision.gameObject.tag == "Balloon")
        {
            if(currentBalloons < maxBalloons)
            {
                currentBalloons++;
                Destroy(collision.gameObject);
            }
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
