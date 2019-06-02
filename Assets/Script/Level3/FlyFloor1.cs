using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFloor1 : MonoBehaviour {

    private Vector3 init;
    public Vector3 target;
    public float speed;
    bool toTarget = true;

	// Use this for initialization
	void Start () {
        init = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(toTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if(transform.position == target)
                toTarget = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, init, speed * Time.deltaTime);
            if (transform.position == init)
                toTarget = true;
        }
	}
}
