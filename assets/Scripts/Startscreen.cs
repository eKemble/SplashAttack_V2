using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startscreen : MonoBehaviour {
    public Canvas numberofplayers;
 



	// Use this for initialization
	void Start ()
    {
        numberofplayers = numberofplayers.GetComponent<Canvas>();
        numberofplayers.enabled = false;

	
	}
	
	// Update is called once per frame
	void Update () {
        startscreenstart();
        numberselect();






    }
    // pull up player select screen

    void startscreenstart()
    {
        if(Input.GetKeyDown("joystick button 9"))
        {
            numberofplayers.enabled = true;
        }

    }
    //selecting number of players
   void numberselect()
    {
        if (numberofplayers.enabled == true)
        {
            if (Input.GetKeyDown("joystick 1 button 0"))
            {
                Player.playersNum = 2;
                SceneManager.LoadScene("BrendonLevelTestScene");
            }
            if (Input.GetKeyDown("joystick 1 button 1"))
            {
                Player.playersNum = 3;
                SceneManager.LoadScene("BrendonLevelTestScene");
            }
            if (Input.GetKeyDown("joystick 1  button 2"))
            {
                Player.playersNum = 4;
                SceneManager.LoadScene("BrendonLevelTestScene");
            }
        }
       
    }
}
