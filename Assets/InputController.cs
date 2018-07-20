using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.F)) {
            if (Physics.SphereCast(transform.position, 2f, transform.forward, out hit))
            {
                print(hit.collider.name);
            }
        }
	}
}
