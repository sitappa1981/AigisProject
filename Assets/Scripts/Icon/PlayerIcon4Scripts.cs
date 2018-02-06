using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon4Scripts : MonoBehaviour {

    public bool[] Player = new bool[3];     //各アイコンとの接触判定
    public GameObject my;

    //テスト
    public GameObject test;
    public GameObject Sub;
    public GameObject GameController;           //元のスプリクトを格納する場所
    SpriteRenderer MainSpriteRenderer;          //SpriteRendererを変更する際に使用
    SpriteRenderer SubSpriteRenderer;
    public Sprite TestIcon;
    public Text[] TestText = new Text[3];
    //テスト終わり

    // Use this for initialization
    void Start () {
        //各アイコンとの接触判定を偽にする
        for (int i=0; i < 3; i++) {
            Player[i] = false;
        }
        //テスト
        MainSpriteRenderer = test.GetComponent<SpriteRenderer>();
        SubSpriteRenderer = Sub.GetComponent<SpriteRenderer>();
        //テスト終わり

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // ドラックしている時に呼ばれる.
    public void OnMouseDrag() {
        Vector3 objectPointInScreen = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePointInScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPointInScreen.z);
        Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScreen);
        mousePointInWorld.z = this.transform.position.z;
        this.transform.position = mousePointInWorld;
    }

    //目標に進入した時に判定
    public void OnTriggerEnter2D(Collider2D collision) {
        //もし0番のアイコンと重なった時
        if (collision.gameObject.tag == "PlayerIcon0") {
            //接触判定を真にする
            Player[0] = true;
        } else if (collision.gameObject.tag == "PlayerIcon1") {
            //接触判定を真にする
            Player[1] = true;
        } else if (collision.gameObject.tag == "PlayerIcon2") {
            //接触判定を真にする
            Player[2] = true;
        }
    }

    //目標から脱出した時に判定
    public void OnTriggerExit2D(Collider2D collision) {
        //もし0番のアイコンとの重なりが解除された時
        if (collision.gameObject.tag == "PlayerIcon0") {
            //接触判定を偽にする
            Player[0] = false;
        } else if (collision.gameObject.tag == "PlayerIcon1") {
            //接触判定を偽にする
            Player[1] = false;
        } else if (collision.gameObject.tag == "PlayerIcon2") {
            //接触判定を偽にする
            Player[2] = false;
        }
    }

    //マウスのボタンを離した時の処理
    public void OnMouseUp() {
        //0番のアイコンの判定が真のとき
        if (Player[0]) {
            //アイコンの場所を元に戻して処理を開始する
            my.transform.position = new Vector3(4.5f, -3.6f, 0f);
            GameController.SendMessage("CharaChange0and4", SendMessageOptions.DontRequireReceiver);

        //1番のアイコンの判定が真のとき
        } else if(Player[1]) {
            //アイコンの場所を元に戻して処理を開始する
            my.transform.position = new Vector3(4.5f, -3.6f, 0f);
            GameController.SendMessage("CharaChange1and4", SendMessageOptions.DontRequireReceiver);
        //2番のアイコンの判定が真のとき
        } else if (Player[2]) {
            //アイコンの場所を元に戻して処理を開始する
            my.transform.position = new Vector3(4.5f, -3.6f, 0f);
            GameController.SendMessage("CharaChange2and4", SendMessageOptions.DontRequireReceiver);
            //アイコンと接触していない時
        } else {
            //アイコンの場所を元に戻す
            my.transform.position = new Vector3(4.5f, -3.6f, 0f);
        }
    }
}
