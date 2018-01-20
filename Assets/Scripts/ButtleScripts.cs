using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtleScripts : MonoBehaviour {

    //プレイヤー関係のステータス格納
    public GameObject[] Player = new GameObject[5]; // プレイヤーキャラクターのオブジェクト格納場所
    public GameObject[] UnitIcon = new GameObject[5];   // プレイヤーアイコンのオブジェクト格納場所
    public static int movecheck;                    // 0or1.移動中(左右に傾く) 2.待機中(傾けを解除) 3.戦闘中(動かさない)
    public float MoveingSpeed;                      // 傾けの速度

    public static int[] PlayerNo = new int[5];      //ステータスを格納するナンバー

    public static int[] Player0ST = new int[4];      //プレイヤー0のステータスを格納する場所 0.最大HP 1.残りHP 2.攻撃力 3.防御力
    public static int[] Player1ST = new int[4];      //プレイヤー1のステータスを格納する場所 0.最大HP 1.残りHP 2.攻撃力 3.防御力
    public static int[] Player2ST = new int[4];      //プレイヤー2のステータスを格納する場所 0.最大HP 1.残りHP 2.攻撃力 3.防御力
    public static int[] Player3ST = new int[4];      //プレイヤー3のステータスを格納する場所 0.最大HP 1.残りHP 2.攻撃力 3.防御力　プレイヤー3はサブメンバー
    public static int[] Player4ST = new int[4];      //プレイヤー4のステータスを格納する場所 0.最大HP 1.残りHP 2.攻撃力 3.防御力　プレイヤー4はサブメンバー

    public static int[,] PlayerST = new int[5,4];   //プレイヤーのステータス関係　前の値はプレイヤー番号、後ろの番号は種類。 0.最大HP 1.残りHP 2.攻撃力 3.防御力




    public Text[] PlayerName = new Text[5];          //各プレイヤーキャラクターの名前を格納
    public Text[] PlayerMAXHP = new Text[5];         //各プレイヤーのMAXHPを格納
    public Text[] PlayerHP = new Text[5];            //各プレイヤーのHPを格納

    public static int[] Damage = new int[4];         //ダメージを格納する場所 0:敵へのダメージ 1～3:味方0～2のダメージ

    //プレイヤーのスプライトの格納場所
    SpriteRenderer[] PlayerSprite = new SpriteRenderer[5];
    //プレイヤーアイコンのスプライトの格納場所
    SpriteRenderer[] PlayerIconSprite = new SpriteRenderer[5];

    SpriteRenderer[] PlayerBox = new SpriteRenderer[5];

    //各ユニットのスプライトの格納場所
    public Sprite[] PlayerPicture = new Sprite[4];
    public Sprite[] IconPicture = new Sprite[4];

    //敵関係のステータス格納
    public GameObject[] Enemy = new GameObject[3];      //敵キャラクターのオブジェクトの格納場所
    public GameObject[] EnemyIcon = new GameObject[1];  //敵アイコンのオブジェクトの格納場所
    public GameObject[] BossIcon = new GameObject[1];   //敵ボスのオブジェクトの格納場所
    public Text EnemyHPText;                        //敵キャラクターのHPの格納場所
    public static int[] EnemyST = new int[3];       //敵のステータスを格納する場所 0.残りHP 1.攻撃力 2.防御力
    public static int EnemyNo;                      //衝突した敵の番号

    //戦闘関係の処理
    public static int GameStatus;                   //ゲームの進捗管理  0.開始準備 1.移動中 2.戦闘準備中 3.戦闘中 4.戦闘終了後
    public GameObject SubUI_EnemyDamageText;        //敵のダメージ処理時のオブジェクト
    public GameObject[] SubUI_PlayerDamageText = new GameObject[3]; //プレイヤーのダメージ処理時のオブジェクト
    public bool buttleContinuation;                 //戦闘継続するかどうかの真偽値

    //それ以外の処理関係
    public GameObject[] Bush = new GameObject[3];   //草表示
    bool BushMate;                                  //草表示を待つ真偽値

    string PlayerTest;

    // Use this for initialization
    void Start () {
        //ゲームの進捗を0にする
        GameStatus = 0;
        //傾け判定を始める
        movecheck = 0;
        //傾けの時間の初期値を設定する
        MoveingSpeed = Random.Range(0.5f, 0.8f);

        //スプライトレンダラーをそれぞれ編集可能状態にする
        for (int i = 0; i < 5; i++) {
            PlayerSprite[i] = Player[i].GetComponent<SpriteRenderer>();
            PlayerIconSprite[i] = UnitIcon[i].GetComponent<SpriteRenderer>();
        }



        //***************//
        //仮の編成
        //***************//
        PlayerNo[0] = 1001;     //ソーマ
        PlayerNo[1] = 1002;     //フィリス
        PlayerNo[2] = 1003;     //ベルニス
        PlayerNo[3] = 1004;     //ヴァレリー
        PlayerNo[4] = 1004;     //ヴァレリー
        Enemy[0].SetActive(false);  //敵オブジェクトを非表示
        EnemyHPText.text = "";      //敵HPの値を白紙にする
        //草表示する判定を真にする
        BushMate = true;
        //***************//
        //仮の編成ここまで
        //***************//





        //初期ステータスを代入する
        StatusInit();
    }

    // Update is called once per frame
    void Update () {
        switch (GameStatus) {
            //準備中
            case 0:
                break;
            //移動中
            case 1:
                //傾け関係の処理を行う
                StartCoroutine("moveing");
                if (BushMate) {
                    //草生やす処理を行う
                    StartCoroutine("BushSpone");
                }
                break;

            //戦闘準備中
            case 2:
                //傾け処理を停止
                movecheck = 2;
                //攻撃可能判定を真にする
                buttleContinuation = true;
                //傾け関係の処理を行う
                StartCoroutine("moveing");
                break;

            //戦闘中
            case 3:
                //攻撃可能判定を真の場合は
                if (buttleContinuation) {
                    //戦闘処理の実行を行う
                    StartCoroutine("ButtleLoop");
                }
                break;
            //戦闘終了
            case 4:
                break;

        }
    }

    //ステータスを設定する
    public void StatusInit() {
        for (int i = 0;i < 5; i++) {
            //プレイヤー0のステータス情報
            switch (PlayerNo[i]) {
                case 1000:
                    break;
                //ソーマの場合
                case 1001:
                    //最大HP、残りHP、攻撃力、防御力に代入する
                    PlayerST[i,0] = StatusInformation.soma[0];
                    PlayerST[i,1] = StatusInformation.soma[0];
                    PlayerST[i,2] = StatusInformation.soma[1];
                    PlayerST[i,3] = StatusInformation.soma[2];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "ソーマ";
                    //画像データを入れ替える
                    PlayerSprite[i].sprite = PlayerPicture[0];
                    break;

                //フィリスの場合
                case 1002:
                    //最大HP、残りHP、攻撃力、防御力に代入する
                    PlayerST[i, 0] = StatusInformation.Phylis[0];
                    PlayerST[i, 1] = StatusInformation.Phylis[0];
                    PlayerST[i, 2] = StatusInformation.Phylis[1];
                    PlayerST[i, 3] = StatusInformation.Phylis[2];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "フィリス";
                    //画像データを入れ替える
                    PlayerSprite[i].sprite = PlayerPicture[1];
                    break;

                //ベルニスの場合
                case 1003:
                    //最大HP、残りHP、攻撃力、防御力に代入する
                    PlayerST[i, 0] = StatusInformation.Berunice[0];
                    PlayerST[i, 1] = StatusInformation.Berunice[0];
                    PlayerST[i, 2] = StatusInformation.Berunice[1];
                    PlayerST[i, 3] = StatusInformation.Berunice[2];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "ベルニス";
                    //画像データを入れ替える
                    PlayerSprite[i].sprite = PlayerPicture[2];
                    break;

                //ヴァレリーの場合
                case 1004:
                    //最大HP、残りHP、攻撃力、防御力に代入する
                    PlayerST[i, 0] = StatusInformation.Valerie[0];
                    PlayerST[i, 1] = StatusInformation.Valerie[0];
                    PlayerST[i, 2] = StatusInformation.Valerie[1];
                    PlayerST[i, 3] = StatusInformation.Valerie[2];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "ヴァレリー";
                    //画像データを入れ替える
                    PlayerSprite[i].sprite = PlayerPicture[3];
                    break;
            }
        }



        //各キャラクターのHPとMAXHPを格納
        PlayerMAXHP[0].text = "/ " + PlayerST[0,0];
        PlayerMAXHP[1].text = "/ " + PlayerST[1,0];
        PlayerMAXHP[2].text = "/ " + PlayerST[2,0];
        PlayerHP[0].text = "" + PlayerST[0, 0];
        PlayerHP[1].text = "" + PlayerST[1, 0];
        PlayerHP[2].text = "" + PlayerST[2, 0];

        GameStatus = 1;
    }

    //戦闘準備を行うスクリプト
    public void ButtleReserve() {
        //敵オブジェクトを表示
        Enemy[0].SetActive(true);
        //敵HPを表示
        EnemyHPText.text = "HP " + EnemyST[0];
    }

    //**********************//
    //　コルーチン処理関係　//
    //**********************//

    //傾けに関する処理
    IEnumerator moveing() {
        //状況次第で変更する
        switch (movecheck) {
            case 0:
                iTween.RotateTo(Player[0], iTween.Hash("z", 15, "islocal", true, "time", MoveingSpeed));
                iTween.RotateTo(Player[1], iTween.Hash("z", 15, "islocal", true, "time", MoveingSpeed));
                iTween.RotateTo(Player[2], iTween.Hash("z", 15, "islocal", true, "time", MoveingSpeed));
                //1.0秒待つ
                yield return new WaitForSeconds(MoveingSpeed);
                movecheck = 1;
                break;

            case 1:
                iTween.RotateTo(Player[0], iTween.Hash("z", -15, "islocal", true, "time", MoveingSpeed));
                iTween.RotateTo(Player[1], iTween.Hash("z", -15, "islocal", true, "time", MoveingSpeed));
                iTween.RotateTo(Player[2], iTween.Hash("z", -15, "islocal", true, "time", MoveingSpeed));
                //1.0秒待つ
                yield return new WaitForSeconds(MoveingSpeed);
                movecheck = 0;
                break;

            case 2:
                iTween.RotateTo(Player[0], iTween.Hash("z", 0, "islocal", true, "time", MoveingSpeed));
                iTween.RotateTo(Player[1], iTween.Hash("z", 0, "islocal", true, "time", MoveingSpeed));
                iTween.RotateTo(Player[2], iTween.Hash("z", 0, "islocal", true, "time", MoveingSpeed));
                //1.0秒待つ
                yield return new WaitForSeconds(MoveingSpeed);
                GameStatus = 3;
                movecheck = 3;
                break;

            default:
                break;
        }
    }

    //戦闘処理のループ
    IEnumerator ButtleLoop() {
        //攻撃可能判定を偽にする
        buttleContinuation = false;
        Debug.Log("EnemyST[1]: " + EnemyST[1]);
        //2.0秒待つ
        yield return new WaitForSeconds(2.0f);
        //プレイヤー側の攻撃力を算出
        int ATC = PlayerST[0, 2] + PlayerST[1, 2] + PlayerST[2, 2];
        //プレイヤー側の防御力を算出
        int DEF = PlayerST[0, 3] + PlayerST[1, 3] + PlayerST[2, 3];
        //プレイヤーと敵のどちらのステータスが優れているかの判定
        bool ButtleAdv;
        //プレイヤー側の攻撃力と防御力の合計が敵の攻撃力と防御力の合計より上の場合は
        if ((ATC + DEF) >= (EnemyST[1] + EnemyST[2])) {
            //プレイヤー有利判定が真になる
            ButtleAdv = true;
            Debug.Log("有利判定");

            Debug.Log("プレイヤー0の攻撃力; " + PlayerST[0, 2] + "プレイヤー0の防御力; " + PlayerST[0, 3]);
            Debug.Log("プレイヤー1の攻撃力; " + PlayerST[1, 2] + "プレイヤー0の防御力; " + PlayerST[1, 3]);
            Debug.Log("プレイヤー2の攻撃力; " + PlayerST[2, 2] + "プレイヤー0の防御力; " + PlayerST[2, 3]);

            //敵の方が上の場合は
        } else {
            //有利判定を偽にする
            ButtleAdv = false;
            Debug.Log("不利判定");
        }
        //有利判定の時の計算方法は
        if (ButtleAdv) {
            //プレイヤーの攻撃ターン
            //攻撃力に1.2倍した値を敵の防御力を引いてダメージ量を計算する
            Damage[0] = (int)(ATC * 1.2) - EnemyST[2];
            //敵のHPをダメージ分差し引く
            EnemyST[0] -= Damage[0];
            //ダメージの値を生成
            Instantiate(SubUI_EnemyDamageText, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

            //敵HPが0以下の場合は
            if (EnemyST[0] <= 0) {
                //敵HPを白紙表示
                EnemyHPText.text = "";
                //敵オブジェクトを不透明にする
                Enemy[0].SetActive(false);
                //敵HPが1以上の場合は
            } else {
                //敵HPを表示
                EnemyHPText.text = "HP " + EnemyST[0];
            }

            //敵の攻撃ターン
            //各プレイヤーへのダメージを算出する
            Damage[1] = EnemyST[1] - PlayerST[0, 3];
            Damage[2] = EnemyST[1] - PlayerST[1, 3];
            Damage[3] = EnemyST[1] - PlayerST[2, 3];
            //味方のHPをダメージ分差し引く
            PlayerST[0, 1] -= Damage[1];
            PlayerST[1, 1] -= Damage[2];
            PlayerST[2, 1] -= Damage[3];
            //ダメージの値を生成
            Instantiate(SubUI_PlayerDamageText[0], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            Instantiate(SubUI_PlayerDamageText[1], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            Instantiate(SubUI_PlayerDamageText[2], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            //プレイヤーHPが0以下の場合は
            if (Player0ST[1] <= 0) {
                PlayerHP[0].text = "" + PlayerST[0, 1];
                PlayerHP[1].text = "" + PlayerST[1, 1];
                PlayerHP[2].text = "" + PlayerST[2, 1];
                //プレイヤーのHPが0以上の場合は
            } else {
                PlayerHP[0].text = "" + PlayerST[0, 1];
                PlayerHP[1].text = "" + PlayerST[1, 1];
                PlayerHP[2].text = "" + PlayerST[2, 1];
            }
            //不利判定の時の計算方法は
        } else {
            //攻撃力を敵の防御力を引いてダメージ量を計算する
            Damage[0] = ATC - EnemyST[2];
            //敵のHPをダメージ分差し引く
            EnemyST[0] -= Damage[0];
            //出現させる座標を設定
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
            //ダメージの値を生成
            Instantiate(SubUI_EnemyDamageText, pos, Quaternion.identity);

            //敵HPが0以下の場合は
            if (EnemyST[0] <= 0) {
                //敵HPを白紙表示
                EnemyHPText.text = "";
                //敵オブジェクトを不透明にする
                Enemy[0].SetActive(false);
                //敵HPが1以上の場合は
            } else {
                //敵HPを表示
                EnemyHPText.text = "HP " + EnemyST[0];
            }
            //敵の攻撃ターン
            //各プレイヤーへのダメージを算出する
            Damage[1] = (int)(EnemyST[1] * 1.2) - PlayerST[0, 3];
            Damage[2] = (int)(EnemyST[1] * 1.2) - PlayerST[1, 3];
            Damage[3] = (int)(EnemyST[1] * 1.2) - PlayerST[2, 3];
            //味方のHPをダメージ分差し引く
            PlayerST[0, 1] -= Damage[1];
            PlayerST[1, 1] -= Damage[2];
            PlayerST[2, 1] -= Damage[3];
            //ダメージの値を生成
            Instantiate(SubUI_PlayerDamageText[0], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            Instantiate(SubUI_PlayerDamageText[1], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            Instantiate(SubUI_PlayerDamageText[2], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            //プレイヤーHPが0以下の場合は
            if (Player0ST[1] <= 0) {
                PlayerHP[0].text = "";
                PlayerHP[1].text = "";
                PlayerHP[2].text = "";
                //プレイヤーのHPが0以上の場合は
            } else {
                //各キャラのHPを表示する
                PlayerHP[0].text = "" + PlayerST[0, 1];
                PlayerHP[1].text = "" + PlayerST[1, 1];
                PlayerHP[2].text = "" + PlayerST[2, 1];
            }
        }
        //敵のHPが0以下の場合
        if (EnemyST[0] <= 0) {
            //1.0秒待つ
            yield return new WaitForSeconds(1.0f);
            //攻撃可能判定を偽にする
            buttleContinuation = false;
            //敵アイコン消滅
            EnemyDefeat();
            //進捗を移動に変更する
            GameStatus = 1;

        } else {
            //攻撃可能判定を真にする
            buttleContinuation = true;
        }
    }

    //草を生成する
    IEnumerator BushSpone() {
        //草生成を一旦止める
        BushMate = false;
        //それぞれのレーンにて対応
        for (int i = 0; i < 3; i++) {
            //0から99までの値をランダムで代入
            int init = Random.Range(0, 100);
            //ランダムで生成された値が0から4の場合(5%)
            if (init < 5) {
                //草が生成される
                Instantiate(Bush[i], new Vector3(-8.0f, (1.0f - i * 1.5f ), 0.0f), Quaternion.identity);
            }
        }
        //1秒待つ
        yield return new WaitForSeconds(1.0f);
        //草生成を再開
        BushMate = true;
    }


    //敵撃破時の処理
    public void EnemyDefeat() {
        switch (EnemyNo) {
            //0番の時
            case 0:
                EnemyIcon[0].SendMessage("EnemyDefeat00", SendMessageOptions.DontRequireReceiver);
                movecheck = 0;
                UnitPosScripts.goinit = true;
                GameStatus = 1;
                break;


            //例外処理
            default:
                break;
        }
    }

    //各種キャラチェンジシステムのスプリクト

    // 0と3をチェンジする時
    public void CharaChange0and3() {
        //主キーとなる番号を入れ替える
        int idx = PlayerNo[0];
        PlayerNo[0] = PlayerNo[3];
        PlayerNo[3] = idx;

        //最大HP、残りHP、攻撃力、防御力を入れ替える
        for (int i = 0; i < 4; i++) {
            int ida = PlayerST[0, i];
            PlayerST[0, i] = PlayerST[3, i];
            PlayerST[3, i] = ida;
        }

        //各キャラクターのHPとMAXHPを格納
        PlayerMAXHP[0].text = "/ " + PlayerST[0,0];
        PlayerHP[0].text = "" + PlayerST[0,1];

        //名前を変更する
        NameChenge();
    }

    // 0と4をチェンジする時
    public void CharaChange0and4() {
        //主キーとなる番号を入れ替える
        int idx = PlayerNo[0];
        PlayerNo[0] = PlayerNo[4];
        PlayerNo[4] = idx;

        //最大HP、残りHP、攻撃力、防御力を入れ替える
        for (int i = 0; i < 4; i++) {
            int ida = PlayerST[0, i];
            PlayerST[0, i] = PlayerST[4, i];
            PlayerST[4, i] = ida;
        }

        //各キャラクターのHPとMAXHPを格納
        PlayerMAXHP[0].text = "/ " + PlayerST[0, 0];
        PlayerHP[0].text = "" + PlayerST[0, 1];

        //名前を変更する
        NameChenge();
    }

    // 1と3をチェンジする時
    public void CharaChange1and3() {
        //主キーとなる番号を入れ替える
        int idx = PlayerNo[1];
        PlayerNo[1] = PlayerNo[3];
        PlayerNo[3] = idx;

        //最大HP、残りHP、攻撃力、防御力を入れ替える
        for (int i = 0; i < 4; i++) {
            int ida = PlayerST[1, i];
            PlayerST[1, i] = PlayerST[3, i];
            PlayerST[3, i] = ida;
        }

        //各キャラクターのHPとMAXHPを格納
        PlayerMAXHP[1].text = "/ " + PlayerST[1, 0];
        PlayerHP[1].text = "" + PlayerST[1, 1];

        //名前を変更する
        NameChenge();
    }


    // 1と4をチェンジする時
    public void CharaChange1and4() {
        //主キーとなる番号を入れ替える
        int idx = PlayerNo[1];
        PlayerNo[1] = PlayerNo[4];
        PlayerNo[4] = idx;

        //最大HP、残りHP、攻撃力、防御力を入れ替える
        for (int i = 0; i < 4; i++) {
            int ida = PlayerST[1, i];
            PlayerST[1, i] = PlayerST[4, i];
            PlayerST[4, i] = ida;
        }

        //各キャラクターのHPとMAXHPを格納
        PlayerMAXHP[1].text = "/ " + PlayerST[1, 0];
        PlayerHP[1].text = "" + PlayerST[1, 1];

        //名前を変更する
        NameChenge();
    }


    // 2と3をチェンジする時
    public void CharaChange2and3() {
        //主キーとなる番号を入れ替える
        int idx = PlayerNo[2];
        PlayerNo[2] = PlayerNo[3];
        PlayerNo[3] = idx;

        //最大HP、残りHP、攻撃力、防御力を入れ替える
        for (int i = 0; i < 4; i++) {
            int ida = PlayerST[2, i];
            PlayerST[2, i] = PlayerST[3, i];
            PlayerST[3, i] = ida;
        }

        //各キャラクターのHPとMAXHPを格納
        PlayerMAXHP[2].text = "/ " + PlayerST[2, 0];
        PlayerHP[2].text = "" + PlayerST[2, 1];

        //名前を変更する
        NameChenge();
    }


    // 2と4をチェンジする時
    public void CharaChange2and4() {
        //主キーとなる番号を入れ替える
        int idx = PlayerNo[2];
        PlayerNo[2] = PlayerNo[4];
        PlayerNo[4] = idx;

        //最大HP、残りHP、攻撃力、防御力を入れ替える
        for (int i = 0; i < 4; i++) {
            int ida = PlayerST[2, i];
            PlayerST[2, i] = PlayerST[4, i];
            PlayerST[4, i] = ida;
        }

        //各キャラクターのHPとMAXHPを格納
        PlayerMAXHP[2].text = "/ " + PlayerST[2, 0];
        PlayerHP[2].text = "" + PlayerST[2, 1];

        //名前を変更する
        NameChenge();
    }


    //名前、画像変更の時
    public void NameChenge() {
        for (int i = 0; i < 5; i++) {
            //主キーで選択する
            switch (PlayerNo[i]) {
                case 1001:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[0];
                    PlayerSprite[i].sprite = PlayerPicture[0];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "ソーマ";
                    break;

                case 1002:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[1];
                    PlayerSprite[i].sprite = PlayerPicture[1];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "フィリス";
                    break;

                case 1003:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[2];
                    PlayerSprite[i].sprite = PlayerPicture[2];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "ベルニス";
                    break;

                case 1004:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[3];
                    PlayerSprite[i].sprite = PlayerPicture[3];
                    //プレイヤーネームを格納
                    PlayerName[i].text = "ヴァレリー";
                    break;
            }
        }
    }
}
