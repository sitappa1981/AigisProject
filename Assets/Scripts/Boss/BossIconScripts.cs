using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//最初のボス
public class BossIconScripts : MonoBehaviour {

    GameObject GameController;                 //ゲーム情報を管理する場所

    //ボスステータス(HP AT DF)
    public int[] Enemy = new int[] { 100, 30, 20 };

    // Use this for initialization
    void Start() {
        //ゲーム情報格納場所を呼び出す
        GameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update() {

    }

    //プレイヤーアイコンと衝突した時
    private void OnTriggerEnter2D(Collider2D other) {
        //敵オブジェクトと衝突した場合
        if (other.gameObject.tag == "Player") {
            //敵ステータスをバトルスプリクトに代入
            for (int i = 0; i < 3; i++) {
                ButtleScripts.EnemyST[i] = Enemy[i];
            }
            GameController.SendMessage("ButtleReserve", SendMessageOptions.DontRequireReceiver);
        }
    }

    //1番のアイコンの敵を撃破した時
    public void EnemyDefeat00() {
        //自分を削除
        Destroy(gameObject);
    }



}