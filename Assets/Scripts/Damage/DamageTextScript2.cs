using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ダメージ表記を消すスプリクト
public class DamageTextScript2 : MonoBehaviour {

    // Use this for initialization
    void Start () {
        //1秒後に消す
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update () {
    }

    //文字情報を変更するスクリプト
}
