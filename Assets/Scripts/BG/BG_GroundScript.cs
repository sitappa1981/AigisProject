using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_GroundScript : MonoBehaviour {

    public GameObject BG_Ground;        //地面のオブジェクト

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //移動中以外は動かさない
        if (ButtleScripts.GameStatus != 1) return;
        //地面を右に動かす
        BG_Ground.transform.Translate(+0.1f,0.0f,0.0f);
        //地面が一定以上動いたら元に戻す
        if (BG_Ground.transform.position.x > 20.0f) {
            BG_Ground.transform.position = new Vector3(0.0f,0.0f,0.0f);
        }
	}
}
