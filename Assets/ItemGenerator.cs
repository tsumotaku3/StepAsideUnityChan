using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //Unityちゃんを入れる
    private GameObject unitychan;
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 30;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すX方向の範囲
    private float posRange = 3.4f;
    //アイテムの誤爆防止
    private bool IsFirstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの移動距離に合わせてアイテムを生成する
        if (((int)this.unitychan.transform.position.z+this.startPos) % 15 == 0 && this.unitychan.transform.position.z + this.startPos + 50 <= this.goalPos)
        {
            if (IsFirstTime)
            {
                IsFirstTime = false;
                //どのアイテムを出すかをランダムに設定
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンを一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, this.unitychan.transform.position.z + 50 + this.startPos);
                    }
                }
                else
                {
                    //レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        //60%コイン配置:30%車配置:10%何もなし
                        if (1 <= item && item <= 6)
                        {
                            //コインを生成
                            GameObject coin = Instantiate(coinPrefab);
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, unitychan.transform.position.z + 50 + this.startPos + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            //車を生成
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, unitychan.transform.position.z + 50 + this.startPos + offsetZ);
                        }
                    }
                }
            }
        }
        else
        {
            IsFirstTime = true;
        }
    }
}
