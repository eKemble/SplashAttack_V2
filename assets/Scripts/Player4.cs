using UnityEngine;
using System.Collections;

public class Player4 : Player {
    void FixedUpdate()
    {
        float controllerHorizontal = Input.GetAxis("Controller4Horizontal") * 50;
        float airHorizontal = controllerHorizontal * 1.2f;//helps create a parabola affect when combined with the smaller jump height
        float parabolaJump = jumpingForce * .7f;
        Vector3 dir;
        float vel;
        float dist;
        float a;

        //---------------------------------JUMP--------------------------------

        //If you press up and you are against a surface and you aren't holding the stall-button
        if (Input.GetKeyDown("joystick 4 button 1") && canJump == true && !stall)
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
                myMovement.y = parabolaJump;
                myMovement.x = airHorizontal;
                myRigidbody.AddForce(myMovement);
                canJump = false; //you can't jump
                inTheAir = true; //air movement is affected
            }
        }


        //-----------------------------------------------------------------------------------------

        //----------------------------------------------MOVING-----------------------------------

        if (controllerHorizontal != 0 /*&& !inTheAir && !stall*/)
        {
            running = true;
            transform.position += new Vector3(controllerHorizontal * moveSpeed * Time.deltaTime, 0, 0);
        }


        //---------------------------------------------------------------------------------------

        //-------------------------STALL MOVEMENT MODIFIERS----------------------------------------------


        if (Input.GetKeyDown("joystick 4 button 3") && canJump == true && bursts > 0)//BIG JUMP
        {
            myMovement.y = jumpingForce * 1.5f;
            myRigidbody.AddForce(myMovement);
            canJump = false;
            inTheAir = true;
            myMovement = Vector3.zero;
            bursts--;
        }

        if (Input.GetKeyDown("joystick 4 button 0") /*CONTROLLER*/&& bursts > 0) //Burst Left
        {
            myMovement.x = -jumpingForce;
            myRigidbody.AddForce(myMovement);
            myMovement = Vector3.zero;
            bursts--;
        }

        if (Input.GetKeyDown("joystick 4 button 2") /*CONTROLLER*/ && bursts > 0)//Burst Right
        {
            myMovement.x = jumpingForce;
            myRigidbody.AddForce(myMovement);
            myMovement = Vector3.zero;
            bursts--;
        }

        if (Input.GetButtonDown("Controller4Fire2"))
        {
            if (currentBalloons > 0)
            {

                GameObject balloon = Instantiate(balloonPrefab) as GameObject;
                Vector3 pos2 = this.transform.position;
                if (right == true)
                {
                    pos2.x += .5f;
                    pos2.y += .5f;
                    balloon.transform.position = pos2;
                    Vector3 pos = this.transform.position;
                    pos.x += 10;
                    pos.y = 0;
                    dir = pos - transform.position;
                    //h = dir.y;
                    dir.y = 0;
                    dist = dir.magnitude;
                    //a = 90 * Mathf.Deg2Rad;
                    //dist += h / Mathf.Tan(a);
                    vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);

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
                    //h = dir.y;
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
}
