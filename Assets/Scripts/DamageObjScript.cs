using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = Random.Range(-0.01f,0.01f);
        float y = Random.Range(-0.01f,0.01f);
        this.transform.position = new Vector3(this.transform.position.x+x, this.transform.position.y+y,0.0f);
	}
}
