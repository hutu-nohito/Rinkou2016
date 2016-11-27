﻿using UnityEngine;
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

//プレイヤーがよく使う変数
public static class PlayerPara
{
    public static float player_speed = 5;
    public static float limmit_speed = 10;
    public static Vector2 start_position = new Vector2(0, 0);
}

public class Player : MonoBehaviour {

    private float speed;             //Floating point variable to store the player's movement speed.
    private float limmit_speed;
    public Vector2 moveDirection = new Vector2(0,0);
    private Vector2 oldposition = new Vector2(0, 0);

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private GameManager gamemanager;//Gamemanagerを参照する

    public GameObject PowewEffect;
    
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Scene内のスクリプト、GameManagerを取得
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        speed = PlayerPara.player_speed;
        limmit_speed = PlayerPara.limmit_speed;

        //Androidで傾きを使う
        Input.gyro.enabled = true;

    }

    void Update()
    {
        //回してるとＵＦＯっぽい
        transform.Rotate(0,0,10);

        //速度制限
        if(rb2d.velocity.magnitude > limmit_speed)
        {
            rb2d.AddForce(-rb2d.velocity * 0.7f);
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
        Vector2 forceV = new Vector2(gravityV.x, gravityV.y) * speed;

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if(Variable.playstate == Utility.PlayState.isPaly){

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.AddForce(movement * 5 * speed);//インプットがなければこっちは0
            rb2d.AddForce(forceV * speed);

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
            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            Variable.count = Variable.count + 1;

            //Update the currently displayed count by calling the SetCountText function.
            TextUtility.SetText(TextUtility.TextName.count, "宝石　" + Variable.count.ToString());
            //gamemanager.SetCountText();
        }

        if (other.gameObject.CompareTag("PowerUP"))
        {
            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //パワーアップ処理
            rb2d.mass *= 1000000;
            speed *= 1000000;
            limmit_speed *= 1;
            PowewEffect.SetActive(true);
        }


    }

}
