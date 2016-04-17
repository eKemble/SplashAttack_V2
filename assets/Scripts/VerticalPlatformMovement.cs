using UnityEngine;
using System.Collections;

public class VerticalPlatformMovement : MonoBehaviour {
    bool down = true;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject platform = this.gameObject;
        Vector3 pos = this.transform.position;
        if (pos.y >= 2)
        {
            down = true;

        }
        if (pos.y <= -2)
        {
            down = false;
        }
        if (down == true)
        {
            pos.y -= .03f;
        }
        if (down == false)
        {
            pos.y += .03f;
        }
        platform.transform.position = pos;
    }
}
