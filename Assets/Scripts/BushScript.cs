using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//草の茂みの移動等に関する処理
public class BushScript : MonoBehaviour {

    /*
     各種設定
     ・プレイヤー１…Position X3 Y-1、Order in Layer -2
     ・プレイヤー２…Position X2 Y0.5、Order in Layer -4
     ・プレイヤー３…Position X3 Y2、Order in Layer -6
         */

    public GameObject Bush;      //ユニットポジションのゲームオブジェクト
    public float BushPosX;       //ユニットポジションのX座標

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        //X座標を常時格納
        BushPosX = Bush.transform.position.x;
        //UnitPosのgoing真の時
        if (UnitPosScripts.goinit) {
            //草を右に動かす
            Bush.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
        }

        if (BushPosX >= 8.5f) {
            Destroy(gameObject);
        }
    }
}
