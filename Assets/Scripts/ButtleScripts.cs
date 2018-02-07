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
    public static int[,] PlayerST = new int[5,5];   //プレイヤーのステータス関係　前の値はプレイヤー番号、後ろの番号は種類。 0.最大HP 1.残りHP 2.物理攻撃力 3.防御力 4,魔法攻撃力
    public static int[,] Buff = new int[3, 6];      //プレイヤーキャラの各バフ 前の値はキャラ番号、後ろの番号は0～2がバフ、3～5がデバフ。0と3が物理攻撃力、1と4が防御力、2と5が魔法攻撃力
    public GameObject[] hpBar = new GameObject[5];  //各キャラクターのHPバー
    float[] Proportion = new float[5];              //各キャラクターの残りHPの割合

    public static int[] Damage = new int[4];         //ダメージを格納する場所 0:敵へのダメージ 1～3:味方0～2のダメージ

    public static bool[] SkillCheck = new bool[5];   //スキルの使用判定

    //プレイヤーのスプライトの格納場所
    SpriteRenderer[] PlayerSprite = new SpriteRenderer[5];
    //プレイヤーアイコンのスプライトの格納場所
    SpriteRenderer[] PlayerIconSprite = new SpriteRenderer[5];

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
            //スプライトレンダラーをそれぞれ編集可能状態にする
            PlayerSprite[i] = Player[i].GetComponent<SpriteRenderer>();
            PlayerIconSprite[i] = UnitIcon[i].GetComponent<SpriteRenderer>();

            //スキルの使用判定を偽にする
            SkillCheck[i] = false;
        }

        //***************//
        //仮の編成
        //***************//
        PlayerNo[0] = 1001;     //ソーマ
        PlayerNo[1] = 1002;     //フィリス
        PlayerNo[2] = 1003;     //ベルニス
        PlayerNo[3] = 1004;     //ヴァレリー
        PlayerNo[4] = 1005;     //アリサ
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
                    PlayerST[i, 4] = StatusInformation.soma[3];
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
                    PlayerST[i, 4] = StatusInformation.Phylis[3];
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
                    PlayerST[i, 4] = StatusInformation.Berunice[3];
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
                    PlayerST[i, 4] = StatusInformation.Valerie[3];
                    //画像データを入れ替える
                    PlayerSprite[i].sprite = PlayerPicture[3];
                    break;
                //ヴァレリーの場合
                case 1005:
                    //最大HP、残りHP、攻撃力、防御力に代入する
                    PlayerST[i, 0] = StatusInformation.Arisa[0];
                    PlayerST[i, 1] = StatusInformation.Arisa[0];
                    PlayerST[i, 2] = StatusInformation.Arisa[1];
                    PlayerST[i, 3] = StatusInformation.Arisa[2];
                    PlayerST[i, 4] = StatusInformation.Arisa[3];
                    //画像データを入れ替える
                    PlayerSprite[i].sprite = PlayerPicture[4];
                    break;
            }
        }
        //各キャラクターのHPバーを設定
        for (int i= 0;i<5;i++) {
            hpBar[i].GetComponent<Image>().fillAmount = 1.0f;
        }
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
        //2.0秒待つ
        yield return new WaitForSeconds(2.0f);

        //必要な処理を一旦記録
        /*
        ベルニスの防御力上昇のバフを実装可能にする
        フィリスの攻撃力上昇のバフを実装可能にする
        ヴァレリーの確定ダメージ量のバフを実装可能にする
        */
        //必要な処理ここまで

        //



        //プレイヤー側の総攻撃力を算出
        int ATC = PlayerST[0, 2] + PlayerST[1, 2] + PlayerST[2, 2] + Buff[0,0] + Buff[1,0] + Buff[2,0] + Buff[0,3] + Buff[1, 3] + Buff[2, 3];

        Debug.Log("攻撃力合計 : " + ATC);
        //プレイヤーの攻撃ターン
        //攻撃力の合計値から敵防御力を差し引いた値が0より大きい場合
        if ((ATC - EnemyST[2]) > 0) {
            //攻撃力に敵の防御力を引いてダメージ量を計算する
            Damage[0] = ATC - EnemyST[2];
        //0以下の場合
        } else {
            //ダメージを0にする(あくまで仮、最低ダメージ保証がある為)
            Damage[0] = 0;
        }


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
        Damage[1] = EnemyST[1] - (PlayerST[0, 3] + Buff[0, 1] + Buff[0, 4]); 
        Damage[2] = EnemyST[1] - (PlayerST[1, 3] + Buff[1, 1] + Buff[1, 4]);
        Damage[3] = EnemyST[1] - (PlayerST[2, 3] + Buff[2, 1] + Buff[2, 4]);
        //味方のHPをダメージ分差し引く
        PlayerST[0, 1] -= Damage[1];
        PlayerST[1, 1] -= Damage[2];
        PlayerST[2, 1] -= Damage[3];
        //ダメージの値を生成
        Instantiate(SubUI_PlayerDamageText[0], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(SubUI_PlayerDamageText[1], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(SubUI_PlayerDamageText[2], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        //プレイヤーHPが0以下の場合は
        if (PlayerST[0,1] <= 0) {

            //仮での値

            //残りHPをHPゲージに適応する
            HP_Proportion();

        //プレイヤーのHPが0以上の場合は
        } else {
            //残りHPをHPゲージに適応する
            HP_Proportion();
        }
        //全員生きてる場合は
        if (EnemyST[0] > 0 && PlayerST[0,1] > 0 && PlayerST[1, 1] > 0 && PlayerST[2, 1] > 0) {
            //再度ループする
            buttleContinuation = true;
        //敵が死んでる場合
        } else {
            //敵アイコンの削除命令
            EnemyIcon[0].SendMessage("EnemyDefeat00", SendMessageOptions.DontRequireReceiver);
            //アイコンの移動を真にする
            UnitPosScripts.goinit = true;
            //傾け処理の値を0にする
            movecheck = 0;
            //進捗管理を移動中にする
            GameStatus = 1;
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

        //スキルの使用判定を入れ替える
        bool skill = SkillCheck[0];
        SkillCheck[0] = SkillCheck[3];
        SkillCheck[3] = skill;

        //最大HP、残りHP、攻撃力、防御力を入れ替える
        for (int i = 0; i < 4; i++) {
            int ida = PlayerST[0, i];
            PlayerST[0, i] = PlayerST[3, i];
            PlayerST[3, i] = ida;
        }

        //残りHPをHPゲージに適応する
        HP_Proportion();

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

        //残りHPをHPゲージに適応する
        HP_Proportion();

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

        //残りHPをHPゲージに適応する
        HP_Proportion();

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

        //残りHPをHPゲージに適応する
        HP_Proportion();

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

        //残りHPをHPゲージに適応する
        HP_Proportion();

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

        //残りHPをHPゲージに適応する
        HP_Proportion();

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
                    break;

                case 1002:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[1];
                    PlayerSprite[i].sprite = PlayerPicture[1];
                    break;

                case 1003:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[2];
                    PlayerSprite[i].sprite = PlayerPicture[2];
                    break;

                case 1004:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[3];
                    PlayerSprite[i].sprite = PlayerPicture[3];
                    break;
                case 1005:
                    //アイコン、立ち絵を変更する
                    PlayerIconSprite[i].sprite = IconPicture[4];
                    PlayerSprite[i].sprite = PlayerPicture[4];
                    break;

            }
        }
    }

    //HPゲージの変更プログラム
    public void HP_Proportion() {
        //プレイヤー0の残りHPをゲージにする
        Proportion[0] = (float)PlayerST[0, 1] / (float)PlayerST[0, 0];
        hpBar[0].GetComponent<Image>().fillAmount = Proportion[0];
        //プレイヤー1の残りHPをゲージにする
        Proportion[1] = (float)PlayerST[1, 1] / (float)PlayerST[1, 0];
        hpBar[1].GetComponent<Image>().fillAmount = Proportion[1];
        //プレイヤー2の残りHPをゲージにする
        Proportion[2] = (float)PlayerST[2, 1] / (float)PlayerST[2, 0];
        hpBar[2].GetComponent<Image>().fillAmount = Proportion[2];
        //プレイヤー3の残りHPをゲージにする
        Proportion[3] = (float)PlayerST[3, 1] / (float)PlayerST[3, 0];
        hpBar[4].GetComponent<Image>().fillAmount = Proportion[3];
        //プレイヤー4の残りHPをゲージにする
        Proportion[4] = (float)PlayerST[4, 1] / (float)PlayerST[4, 0];
        hpBar[4].GetComponent<Image>().fillAmount = Proportion[4];



    }




}
