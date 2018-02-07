using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInformation : MonoBehaviour {

    //////////////////////////////////////////
    //プレイヤーキャラクターのステータス設定
    //{最大HP,物理攻撃力,防御力,魔法攻撃力}
    //////////////////////////////////////////

    //ソーマステータス
    public static int[] soma = new int[] { 500, 50, 0, 0 };
    public static string somaSkill = "ソーマスキル名";

    //フィリスステータス
    public static int[] Phylis = new int[] { 600, 50, 20, 0 };
    public static string PhylisSkill = "フィリススキル名";

    //ベルニスステータス
    public static int[] Berunice = new int[] { 900, 50, 50, 0 };
    public static string BeruniceSkill = "ベルニススキル名";

    //ヴァレリーステータス
    public static int[] Valerie = new int[] { 600, 50, 0, 50 };
    public static string ValerieSkill = "ヴァレリースキル名";

    //アリサステータス
    public static int[] Arisa = new int[] { 500, 20, 0, 0 };
    public static string ArisaSkill = "アリサスキル名";


    ////////////////////////////////////////
    //敵キャラクターのステータス設定
    //{最大HP,物理攻撃力,防御力,魔法攻撃力}
    ////////////////////////////////////////

    //紫ゴブリンステータス
    public static int[] PurpleGoblin = new int[] { 200, 100, 20, 0 };

    ////////////////////////
    //スキルによるバフ効果
    ////////////////////////

    //フィリスのバフ
    public static int[] PhylisBuf = new int[3];

    //ベルニスのバフ
    public static int[] BeruniceBuff = new int[3];

    //ヴァレリーのバフ
    public static int[] ValerieBuff = new int[3];


    void Start() {
        //バフ関係の値をリセット
        for (int i = 0; i <5;i++) {
            //フィリスのバフを白紙にする
            PhylisBuf[i] = 0;
        }
    }
}
