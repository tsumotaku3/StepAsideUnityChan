﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    private GameObject unitychan;
    //Unityちゃんとカメラの距離
    private float difference;
    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        unitychan = GameObject.Find("unitychan");
        //Unityちゃんとカメラの距離(Z座標)を求める
        this.difference = unitychan.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんに合わせてカメラの位置を移動
        this.transform.position = new Vector3(0,this.transform.position.y,this.unitychan.transform.position.z - difference);
    }
}
