using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPosScripts : MonoBehaviour {

    public GameObject UnitPos;      //ユニットポジションのゲームオブジェクト
    public float UnitPosX;          //ユニットポジションのX座標
    static public bool goinit;      //ユニットポジションを動かすかどうかの判定

	// Use this for initialization
	void Start () {
        goinit = true;
    }
	
	// Update is called once per frame
	void Update () {
        //X座標を常時格納
        UnitPosX = UnitPos.transform.position.x;
        //goinitが真の時
        if (goinit) {
            //ユニットポジションを左に動かす
            UnitPos.transform.position += new Vector3(-0.01f,0.0f,0.0f);
        }
	}

    //オブジェクトと衝突した場合
    private void OnTriggerEnter2D(Collider2D other) {
        //敵オブジェクトと衝突した場合
        if (other.gameObject.tag == "Enemy") {
            //動かすかどうかの判定を偽にする
            goinit = false;
        }
        if (other.gameObject.tag == "Finish") {
            //動かすかどうかの判定を偽にする
            goinit = false;
        }
    }
}
