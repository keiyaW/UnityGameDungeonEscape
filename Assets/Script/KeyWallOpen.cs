using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWallOpen : MonoBehaviour {

    public Vector3 Target;

    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, Target, 20f * Time.deltaTime);
    }
}
