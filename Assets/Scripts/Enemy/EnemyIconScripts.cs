using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//紫ゴブリン一体のパターン
public class EnemyIconScripts : MonoBehaviour {

    GameObject GameController;                 //ゲーム情報を管理する場所

    //紫ゴブリンステータス(HP AT DF)
    int[] Enemy = new int[] { 30, 12, 0 };

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
            StartCoroutine("ButtleStart");
        }
    }

    //1番のアイコンの敵を撃破した時
    public void EnemyDefeat00() {
        //自分を削除
        Destroy(gameObject);
    }

    IEnumerator ButtleStart() {
        //敵ステータスをバトルスプリクトに代入
        for (int i = 0; i < 3; i++) {
            ButtleScripts.EnemyST[i] = StatusInformation.PurpleGoblin[i];
        }
        //0.5秒待つ
        yield return new WaitForSeconds(0.5f);
        //戦闘準備に突入する
        ButtleScripts.GameStatus = 2;
        GameController.SendMessage("ButtleReserve", SendMessageOptions.DontRequireReceiver);
    }

}