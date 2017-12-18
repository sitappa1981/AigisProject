using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon3Scripts : MonoBehaviour {

    public bool[] Player = new bool[3];     //各アイコンとの接触判定
    public GameObject my;

    //テスト
    public GameObject test;
    public GameObject Sub;
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
        }
    }

    //目標から脱出した時に判定
    public void OnTriggerExit2D(Collider2D collision) {
        //もし0番のアイコンとの重なりが解除された時
        if (collision.gameObject.tag == "PlayerIcon0") {
            //接触判定を偽にする
            Player[0] = false;
        }
    }

    //マウスのボタンを離した時の処理
    public void OnMouseUp() {
        //0番のアイコンの判定が真のとき
        if (Player[0]) {
            //アイコンの場所を元に戻して処理を開始する
            my.transform.position = new Vector3(1.0f, -4.0f, 0f);

            //テスト
            Debug.Log("処理開始");
            MainSpriteRenderer.sprite = TestIcon;
            SubSpriteRenderer.sprite = TestIcon;
            TestText[0].text = "…誰";
            TestText[1].text = "/ 999";
            TestText[2].text = "999";
            Sub.transform.localScale = new Vector3(2, 2, 2);
            //テスト終了


        } else {
            //アイコンの場所を元に戻す
            my.transform.position = new Vector3(1.0f ,-4.0f ,0f);
        }
    }

}
