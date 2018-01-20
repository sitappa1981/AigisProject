using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player0DamageTextScript : MonoBehaviour {

    private Text DamageText;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    //文字情報を変更するスクリプト
    void Awake() {
        //自身のテキストを変更する準備
        this.DamageText = this.GetComponent<Text>();
        //自身のテキストを変更する
        this.DamageText.text = ButtleScripts.Damage[1] + "";
    }
}
