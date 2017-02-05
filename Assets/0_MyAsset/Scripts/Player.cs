using UnityEngine;
using System.Collections;

using UnityEngine.UI;

/******************************************************************************/
/** @brief 自機の操作
* @date 2016/11/14
* @author 石川
* @param[in] m_fringe 干渉縞の計算結果を格納
* @param[in]
*/
/******************************************************************************/
/* 更新履歴
*   キー入力はデバッグ用
*/
/******************************************************************************/

//マシン性能(選択できるようになったら使う)
public static class PlayerPara
{
    public static float player_acceleration = 5;//加速度
    public static float player_limmit_speed = 25;//最高速
    public static float player_mass = 1;//重さ
    public static float player_power = 1;//攻撃力
    public static float player_friction = 2;//摩擦
    public static float player_dash = 7;//プッシュ加速力
    public static Vector2 start_position = new Vector2(0, 0);
}

public class Player : Machine_Parameter {

    public Vector2 moveDirection = new Vector2(0,0);
    private Vector2 oldposition = new Vector2(0, 0);

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private GameManager gamemanager;//Gamemanagerを参照する

    public GameObject PowewEffect;

    private bool isDash = false;
    private bool isDashCoroutine = false;
    private float dash_time = 0;
    private float dash_needtime = 0.8f;

    private GameObject Save;
    private Sound_Controller SC;

    //SE
    [SerializeField]
    private AudioClip[] SEs;
    private AudioSource[] audio_source;

    //Effect
    [SerializeField]
    private Canvas Canvas_E;
    private Text text_e;
        
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Scene内のスクリプト、GameManagerを取得
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Save = GameObject.FindGameObjectWithTag("Save");
        SC = Save.GetComponent<Sound_Controller>();
        audio_source = GetComponents<AudioSource>();

        text_e = Canvas_E.GetComponentInChildren<Text>();
        text_e.text = "";//消しとく

        //マシン性能の設定
        Machine_Parameter MP = Save.GetComponent<Machine_Parameter>();
        acceleration = MP.acceleration;
        limmit_speed = MP.limmit_speed;
        mass = MP.mass;
        power = MP.power;
        friction = MP.friction;
        dash = MP.dash;
        //ここで見た目(sprite)を変える

        //SE
        audio_source[1].Pause();

        //Androidで傾きを使う
        Input.gyro.enabled = true;

    }

    void Update()
    {
        //回してるとＵＦＯっぽい
        transform.Rotate(0,0,20);

        //速度制限
        if(rb2d.velocity.magnitude > limmit_speed)
        {
            rb2d.AddForce(-rb2d.velocity * 0.7f);
        }

        //プッシュ
        if (Input.GetMouseButton(0))
        {
            if (rb2d.drag < 20)
            {
                rb2d.drag += friction * 0.1f;//摩擦を足していく
            }

            if (!audio_source[1].isPlaying)
            {
                audio_source[1].UnPause();
            }

            audio_source[1].pitch = -rb2d.drag / 10;

            //ボタンを何秒か押してないとダッシュは発動しない
            dash_time += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(1, GetComponent<SpriteRenderer>().color.g - 0.01f, GetComponent<SpriteRenderer>().color.b - 0.01f, 1);//チャージ演出
            if (dash_time > dash_needtime)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            rb2d.drag = 0;

            //SE
            audio_source[1].Pause();
            audio_source[1].pitch = 0;
            audio_source[2].PlayOneShot(SEs[0]);

            if (dash_time > dash_needtime)
            {
                isDash = true;
            }

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            dash_time = 0;
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        // ジャイロから重力の下向きのベクトルを取得。水平に置いた場合は、gravityV.zが-9.8になる.
        Vector3 gravityV = Input.gyro.gravity;
        // 外力のベクトルを計算.
        Vector2 forceV = new Vector2(gravityV.x, gravityV.y) * acceleration;

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if(Variable.playstate == Utility.PlayState.isPaly){

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.AddForce(movement * 5 * acceleration);//インプットがなければこっちは0
            rb2d.AddForce(forceV * acceleration);

            if (isDash)
            {
                rb2d.AddForce(movement * 5 * dash, ForceMode2D.Impulse);
                rb2d.AddForce(forceV * dash,ForceMode2D.Impulse);
                isDash = false;
                dash = 0;
                StartCoroutine(DashReset());
            }

        }

        moveDirection = new Vector2(transform.position.x - oldposition.x, transform.position.y - oldposition.y);
        oldposition = transform.position;
        
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {
            SC.PowerUpSE();
            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            Variable.count = Variable.count + 1;

            //Update the currently displayed count by calling the SetCountText function.
            if (ConfigPara.language != ConfigPara.Language.Japanese)
            {
                TextUtility.SetText(TextUtility.TextName.count, "Jewelry　" + Variable.count.ToString());
            }
            else
            {
                TextUtility.SetText(TextUtility.TextName.count, "宝石　" + Variable.count.ToString());
            }
            //gamemanager.SetCountText();
        }

        if (other.gameObject.CompareTag("PowerUP"))
        {
            //パワーアップ処理
            acceleration += 2 * other.gameObject.GetComponent<Machine_Parameter>().acceleration;
            limmit_speed += 10 * other.gameObject.GetComponent<Machine_Parameter>().limmit_speed;
            mass += 50 * other.gameObject.GetComponent<Machine_Parameter>().mass;
            power += 1 * other.gameObject.GetComponent<Machine_Parameter>().power;
            friction += 5 * other.gameObject.GetComponent<Machine_Parameter>().friction;
            dash += (other.gameObject.GetComponent<Machine_Parameter>().acceleration + other.gameObject.GetComponent<Machine_Parameter>().limmit_speed) / 2;

            //エフェクト(あとで何とか)
            if(other.gameObject.GetComponent<Machine_Parameter>().acceleration > 0)
            {
                if (ConfigPara.language != ConfigPara.Language.Japanese)
                {
                    text_e.text = "AGL";//
                }
                else
                {
                    text_e.text = "カソク";//
                }
                text_e.color = new Color(0, 1, 0, 0.8f);
            }
            if (other.gameObject.GetComponent<Machine_Parameter>().limmit_speed > 0)
            {
                if (ConfigPara.language != ConfigPara.Language.Japanese)
                {
                    text_e.text = "SPD";//
                }
                else
                {
                    text_e.text = "ハヤサ";//消しとく
                }
                text_e.color = new Color(0, 0, 1, 0.8f);
            }
            if (other.gameObject.GetComponent<Machine_Parameter>().power> 0)
            {
                if (ConfigPara.language != ConfigPara.Language.Japanese)
                {
                    text_e.text = "POW";//
                }
                else
                {
                    text_e.text = "パワー";//消しとく
                }
                text_e.color = new Color(1, 0, 0, 0.8f);
            }
            StartCoroutine(PowerUP());
            Destroy(other.gameObject);
        }

    }

    void OnCollisionEnter2D()
    {
        SC.KabeSE();
    }

    IEnumerator PowerUP()
    {

        SC.PowerUpSE();

        //エフェクトがついてたらやらない
        if (PowewEffect.activeInHierarchy) { yield break; }

        PowewEffect.SetActive(true);
        
        yield return new WaitForSeconds(1);

        PowewEffect.SetActive(false);
        text_e.text = "";//消しとく
        text_e.color = new Color(1, 1, 1, 0.8f);

    }

    IEnumerator DashReset()
    {
        if (isDashCoroutine) { yield break; }
        isDashCoroutine = true;

        yield return new WaitForSeconds(dash_needtime);

        dash = 7;//整数に

        isDashCoroutine = false;

    }
}
