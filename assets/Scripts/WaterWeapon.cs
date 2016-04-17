using UnityEngine;
using System.Collections;

public class WaterWeapon : MonoBehaviour {
    ParticleSystem ps;
    public float currentWaterSupply = 12;

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
        if (Input.GetButtonDown("Fire1"))
        {
           if(currentWaterSupply >= 0)
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

    void OnParticleCollision(Collider other)
    {
        ps.Clear();
    }

}
