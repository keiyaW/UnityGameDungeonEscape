using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {

    public GameObject buttonPressed;
    public GameObject buttonWall;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name.Contains("Box"))
        {
            buttonPressed.SetActive(true);
            gameObject.SetActive(false);
            buttonWall.GetComponent<ButtonWallClose>().enabled = false;
            buttonWall.GetComponent<ButtonWallOpen>().enabled = true;
            //buttonWall.transform.Translate(0, 6, 0);
        }
    }
}
