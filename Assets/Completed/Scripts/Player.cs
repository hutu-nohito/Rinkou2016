using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
    public Vector2 moveDirection = new Vector2(0,0);
    private Vector2 oldposition = new Vector2(0, 0);

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private GameManager gamemanager;//Gamemanagerを参照する
    
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Scene内のスクリプト、GameManagerを取得
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {
        transform.Rotate(0,0,10);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if(Variable.playstate == Utility.PlayState.isPaly){

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.AddForce(movement * speed);

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
            Utility.SetText(Utility.TextName.count, "宝石　" + Variable.count.ToString());
            //gamemanager.SetCountText();
        }


    }

}
