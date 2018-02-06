using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpript : MonoBehaviour {

    public float LifeTime = 3.0f; //寿命秒数
    public float Speed = -12.0f; //移動速度

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, 0); //速度を与える
        Destroy(gameObject, LifeTime); //寿命が尽きたら自滅
    }

    // Update is called once per frame
    void Update () {
		
	}
}
