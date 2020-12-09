using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;
    //前方向の速度
    private float velocityZ = 16;
    //横方向の速度
    private float velocityX = 10;
    //上方向の速度（追加）
    private float velocityY = 10f;
    //左右の移動できる範囲
    private float movableRange = 3.4f;
    //動きを減速させる係数
    private float coefficient = 0.99f;
    //ゲーム終了の判定
    private bool IsEnd = false;
    //ゲーム終了時に表示されるテキスト
    private GameObject stateText;
    //スコアを表示するテキスト
    private GameObject scoreText;
    //得点
    private int score;
    //左ボタンの押下の判定
    private bool isLButtonDown;
    //左ボタンの押下の判定
    private bool isRButtonDown;
    //左ボタンの押下の判定
    private bool isJButtonDown;
    // Start is called before the first frame update
    void Start()
    {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを始める
        this.myAnimator.SetFloat("Speed",1);

        //Rigidbodyコンポーネントを取得
        myRigidbody = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了ならUnityちゃんを減衰させる
        if (this.IsEnd)
        {
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.velocityZ *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        //横方向の入力による速度
        float inputVelocityX = 0;
        //上方向の入力による速度（追加）
        float inputVelocityY = 0;
        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左方向への速度を代入
            inputVelocityX = -this.velocityX;
        }
        else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入
            inputVelocityX = this.velocityX;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            myAnimator.SetBool("Jump",true);
            //上方向への速度を代入
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        //Jumpステートの場合はJumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //Unityちゃんに速度を与える
        myRigidbody.velocity = new Vector3(inputVelocityX,inputVelocityY, velocityZ);
    }

    void OnTriggerEnter(Collider other)
    {
        //障害物に当たった場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.IsEnd = true;
            //stateTextにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        //ゴール地点に到達した場合
        if (other.gameObject.tag == "GoalTag")
        {
            this.IsEnd = true;
            //stateTextにGAME CLEARを表示
            this.stateText.GetComponent<Text>().text = "CLEAR!";
        }
        //コインに衝突した場合
        if (other.gameObject.tag == "CoinTag")
        {
            //スコアを加算
            score += 10;
            //scoreTextに獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();
            //衝突したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }
    //ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }
    //ジャンプボタンを離した場合の処理
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }
    //左ボタンを押した場合の処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    //右ボタンを押した場合の処理
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを離した場合の処理
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
