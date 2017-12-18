using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//ドラックアンドドロップの処理を行うテストスプリクト
public class DaDScripts : MonoBehaviour {

    public bool Test; 

	// Use this for initialization
	void Start () {
        Test = false;
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
        if (collision.gameObject.tag == "Player") {
            Debug.Log("正解判定");
        }
        Debug.Log("進入判定はしている");
    }

    //目標から脱出した時に判定
    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("大正解判定");
        }
        Debug.Log("脱出判定はしている");
    }





}