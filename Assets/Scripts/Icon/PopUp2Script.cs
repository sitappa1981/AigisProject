﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp2Script : MonoBehaviour {

    public static bool PopUpInit;
    public GameObject Popup02;

    // Use this for initialization
    void Start () {
        PopUpInit = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //マウスカーソルが上に来ている時に呼び出される
    private void OnMouseEnter() {
        Debug.Log("カーソルが上にある");
        PopUpInit = true;
    }

    //マウスカーソルが上からはずれた時
    private void OnMouseExit() {
        Debug.Log("カーソルが離れた");
        PopUpInit = false;
        Popup02.SetActive(false);
    }
}
