using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageTextScript : MonoBehaviour {

    private Text DamageText;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    //文字情報を変更するスクリプト
    void Awake() {
        this.DamageText = this.GetComponent<Text>();
        this.DamageText.text = ButtleScripts.Damage[0] + "";
    }
}
