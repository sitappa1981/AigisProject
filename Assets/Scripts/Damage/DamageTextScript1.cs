using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DamageTextScript1 : MonoBehaviour {

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
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        textmeshPro.text = ButtleScripts.Damage[0] / 3 + "";
    }
}
