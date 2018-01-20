using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextScript1 : MonoBehaviour {

    private Text DamageText;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        //上へと移動する
        this.transform.position += new Vector3(0f, 2.0f, 0f);
    }

    //文字情報を変更するスクリプト
    void Awake() {
        this.DamageText = this.GetComponent<Text>();
        this.DamageText.text = ButtleScripts.Damage[0] + "";
    }
}
