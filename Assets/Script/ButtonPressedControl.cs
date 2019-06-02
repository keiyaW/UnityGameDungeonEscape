using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedControl : MonoBehaviour {

    public GameObject button;
    public GameObject buttonWall;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.name.Contains("Box"))
        {
            button.SetActive(true);
            gameObject.SetActive(false);
            buttonWall.GetComponent<ButtonWallOpen>().enabled = false;
            buttonWall.GetComponent<ButtonWallClose>().enabled = true;
            //buttonWall.transform.Translate(0, -6, 0);
        }
    }
}
