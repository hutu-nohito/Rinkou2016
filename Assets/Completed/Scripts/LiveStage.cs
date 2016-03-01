using UnityEngine;
using System.Collections;

public class LiveStage : MonoBehaviour {

    //こっから出たら死ぬ

    private Player player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Player"))
        {
            Variable.playstate = Utility.PlayState.Failed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(player.moveDirection.normalized * 20,ForceMode2D.Impulse);
        }
    }

}
