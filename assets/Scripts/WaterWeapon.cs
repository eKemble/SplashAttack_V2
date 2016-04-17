using UnityEngine;
using System.Collections;

public class WaterWeapon : MonoBehaviour {
    ParticleSystem ps;
    public float currentWaterSupply = 12;
    public int dmg =20;
    public int dmgdone;

    void Start ()
    {
        ps = GetComponent<ParticleSystem>();
	}
	
	void Update ()
    {
        shoot();
	}

    void shoot()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("ControllerFire"))
        {
           if(currentWaterSupply > 0)
            {
                ps.Play();
                currentWaterSupply -= 1;                       
            }
        }

    }

    //refill tanks
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "refill")
            currentWaterSupply = 12;
    }

    /*dmg output
    void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.tag == "player1")
        {
            Player1.health -= dmg;
        }
        if (other.gameObject.tag == "player2")
        {
            Player2.S.health -= dmg;
        }
        if (other.gameObject.tag == "player3")
        {
            Player3.S.health -= dmg;
        }
        if (other.gameObject.tag == "player4")
        {
            Player4.S.health -= dmg;
        }
    }*/
        
 
}
