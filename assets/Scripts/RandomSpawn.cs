using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour {
    public float lastTime = 0;
    public int secondsBetweenAttempts = 3;
    public GameObject balloonPrefab;
    public double chance = 0.15;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastTime >= secondsBetweenAttempts)
        {
            if (Random.value <= chance) { 

                GameObject balloon = Instantiate(balloonPrefab) as GameObject;
                Vector3 pos = this.transform.position;
                pos.y -= .5f;
                balloon.transform.position = pos;
            }
            lastTime = Time.time;
        }
	}
}
