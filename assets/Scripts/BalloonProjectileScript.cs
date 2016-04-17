using UnityEngine;
using System.Collections;

public class BalloonProjectileScript : MonoBehaviour
{

   public ParticleSystem pop;
   
    // Use this for initialization
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.contacts[0];
        Vector3 BPos = point.point;
        Quaternion q = new Quaternion(0, 0, 0, 0);
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(pop, BPos, q);
            pop.Play();
            Destroy(this.gameObject);
        }
        //splash effect here
    }

}
