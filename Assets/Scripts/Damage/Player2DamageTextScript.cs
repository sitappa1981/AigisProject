using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//プレイヤー2のダメージテキストの値を代入するスプリクト
[RequireComponent(typeof(TextMeshProUGUI))]
public class Player2DamageTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        //上へと移動する
        this.transform.position += new Vector3(0f, 2.0f, 0f);
    }

    //文字情報を変更するスクリプト
    void Awake() {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        textmeshPro.text = ButtleScripts.Damage[3] + "";
    }
}
