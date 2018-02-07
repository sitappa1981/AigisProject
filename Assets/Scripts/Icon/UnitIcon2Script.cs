using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitIcon2Script : MonoBehaviour {

    public GameObject Popup02;  //ポップアップウィンドウの登録場所

    public GameObject UnitHPBar1;

    public Text Unit_Name;      //ユニット名の格納場所
    public Text Skill_Name;     //スキル名の格納場所
    public Text Skill_Text;     //スキルテキストの格納場所

    public Text Unit_HP;        //ユニットのHPの格納場所
    public Text Unit_A_ATC;     //ユニットの物理攻撃力の格納場所
    public Text Unit_B_ATC;     //ユニットの魔法攻撃力の格納場所
    public Text Unit_DEF;       //ユニットの防御力の格納場所

    public bool SkillTime;      //スキル発動中かどうかの判定

    public static float Timer;  //スキルの残り時間

    // Use this for initialization
    void Start() {
        //スキルが発動していない状態と初期値を設定する
        SkillTime = false;
    }

    // Update is called once per frame
    void Update() {
        //スキル発動時間に関する内容
        if (Timer > 0.0f && SkillTime) {
            //時間経過を行う
            Timer -= Time.deltaTime;
            //時間が経過した場合
        } else if (Timer < 0.0f && SkillTime) {
            //時間を0にする
            Timer = 0.0f;
            //スキル発動を終了させる
            SkillTime = false;
            //バフを解除する
            BuffOut();
        } else {

        }
    }

    //マウスカーソルが上に来ている時に呼び出される
    private void OnMouseEnter() {
        //ポップアップを表示する
        Popup02.SetActive(true);
        //ユニット1のHPが被るので非表示にする
        UnitHPBar1.SetActive(false);
        //ポップアップ内の文字を各キャラクターの文章に適応する
        SkillText();
    }

    //マウスカーソルが離れた時に呼び出される
    private void OnMouseExit() {
        //ポップアップを非表示にする
        Popup02.SetActive(false);
        //ユニット1のHPを表示にする
        UnitHPBar1.SetActive(true);
    }

    //スキルテキストの変更命令
    public void SkillText() {
        //スキル使用後の場合は発動しない
        if (ButtleScripts.SkillCheck[2]) return;
        switch (ButtleScripts.PlayerNo[2]) {
            //ソーマ
            case 1001:
                //キャラ名
                Unit_Name.text = "ソーマ";
                //キャラのスキル名
                Skill_Name.text = "ソーマのスキル名";
                //キャラのスキルテキスト
                Skill_Text.text = "ソーマスキル内容ソーマスキル内容";

                break;

            case 1002:
                //キャラ名
                Unit_Name.text = "フィリス";
                //キャラのスキル名
                Skill_Name.text = "フィリスのスキル名";
                //キャラのスキルテキスト
                Skill_Text.text = "フィリススキル内容フィリススキル内容";
                break;

            case 1003:
                //キャラ名
                Unit_Name.text = "ベルニス";
                //キャラのスキル名
                Skill_Name.text = "ベルニスのスキル名";
                //キャラのスキルテキスト
                Skill_Text.text = "ベルニススキル内容ベルニススキル内容";
                break;

            case 1004:
                //キャラ名
                Unit_Name.text = "ヴァレリー";
                //キャラのスキル名
                Skill_Name.text = "ヴァレリーのスキル名";
                //キャラのスキルテキスト
                Skill_Text.text = "ヴァレリースキル内容ヴァレリースキル内容";
                break;

            case 1005:
                //キャラ名
                Unit_Name.text = "アリサ";
                //キャラのスキル名
                Skill_Name.text = "アリサのスキル名";
                //キャラのスキルテキスト
                Skill_Text.text = "アリサスキル内容アリサスキル内容";
                break;


            //例外処理
            default:
                break;
        }
        //ステータスの共通事項
        //ユニットの残りHP / MAXHP
        Unit_HP.text = "HP " + ButtleScripts.PlayerST[2, 1] + " / " + ButtleScripts.PlayerST[2, 0];
        //ユニットの物理攻撃力
        Unit_A_ATC.text = "物理攻撃力 " + ButtleScripts.PlayerST[2, 2] + "(" + (ButtleScripts.Buff[2, 0] + ButtleScripts.Buff[2, 3]) + ")";
        //ユニットの魔法攻撃力
        Unit_B_ATC.text = "魔法攻撃力 " + ButtleScripts.PlayerST[2, 4] + "(" + (ButtleScripts.Buff[2, 2] + ButtleScripts.Buff[2, 5]) + ")";
        //ユニットの防御力
        Unit_DEF.text = "防御力　　 " + ButtleScripts.PlayerST[2, 3] + "(" + (ButtleScripts.Buff[2, 1] + ButtleScripts.Buff[2, 4]) + ")";
    }

    //アイコンをクリックされた時、スキルを発動させる
    void OnMouseDown() {
        //スキル使用後の場合は発動しない
        if (ButtleScripts.SkillCheck[2]) return;
        //プレイヤー2の所のキャラクターによってスキルが変動
        switch (ButtleScripts.PlayerNo[2]) {
            //ソーマの場合
            case 1001:
                //敵に直接ダメージを与える(一瞬)
                Debug.Log("ソーマスキル発動");
                break;
            //フィリスの場合
            case 1002:
                //自身の攻撃力上昇(仮で10%、10秒)

                //キャラクターのバフ量を算出して格納しておく
                StatusInformation.PhylisBuf[2] = (int)(ButtleScripts.PlayerST[2, 2] / 10);
                //自分のバフ計算に追加する
                ButtleScripts.Buff[2, 0] += StatusInformation.PhylisBuf[2];
                //効果時間は10秒
                Timer = 10.0f;
                //スキル発動判定を真にする
                SkillTime = true;
                break;

            //ベルニスの場合
            case 1003:
                //味方全体の防御力上昇(仮で10%、10秒)

                //キャラクターのバフ量を算出して追加格納しておく
                StatusInformation.BeruniceBuff[2] = (int)(ButtleScripts.PlayerST[2, 3] / 10);
                //全員のバフ計算に追加する
                ButtleScripts.Buff[0, 1] += StatusInformation.BeruniceBuff[2];
                ButtleScripts.Buff[1, 1] += StatusInformation.BeruniceBuff[2];
                ButtleScripts.Buff[2, 1] += StatusInformation.BeruniceBuff[2];

                //効果時間は10秒
                Timer = 10.0f;

                break;
            //ヴァレリーの場合
            case 1004:
                //魔法攻撃力UP(仮で10%、10秒)

                //キャラクターのバフ量を算出して追加格納しておく
                StatusInformation.ValerieBuff[2] = (int)(ButtleScripts.PlayerST[2, 4] / 10);

                //自分のバフ計算に追加する
                ButtleScripts.Buff[2, 2] += StatusInformation.ValerieBuff[2];


                break;

            case 1005:
                //味方のHP回復(一瞬)
                Debug.Log("アリサスキル発動");
                break;

        }
        //スキルテキストを変更する
        SkillText();
        //スキル使用判定を真にする
        ButtleScripts.SkillCheck[2] = true;

    }
    //バフ剥がし
    void BuffOut() {
        //フィリスの分のバフを剥がす
        ButtleScripts.Buff[2, 0] -= StatusInformation.PhylisBuf[2];

        //ベルニスの分のバフを剥がす
        ButtleScripts.Buff[0, 1] -= StatusInformation.BeruniceBuff[2];
        ButtleScripts.Buff[1, 1] -= StatusInformation.BeruniceBuff[2];
        ButtleScripts.Buff[2, 1] -= StatusInformation.BeruniceBuff[2];

        //ヴァレリーの分のバフを剥がす
        ButtleScripts.Buff[2, 2] -= StatusInformation.ValerieBuff[2];


        //バフの加算量は0にする
        StatusInformation.PhylisBuf[2] = 0;
        StatusInformation.BeruniceBuff[2] = 0;
        StatusInformation.ValerieBuff[2] = 0;
    }

}
