using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //カメラを入れる
    private GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        //Cameraのオブジェクトを取得
        this.Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //カメラより後ろに下がったらオブジェクトを消す
        if (this.Camera.transform.position.z >= transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
