using UnityEngine;
using System.Collections;

public class BrokenBlockController : MonoBehaviour {

    public int maxHP = 5;//壊れるまでの回数
    private int carrentHP;
    private bool isbroken = false;

    public GameObject[] brokenPiece;//壊れた時に飛ぶ破片

	// Use this for initialization
	void Start () {

        carrentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(carrentHP <= 0)
        {
            if (!isbroken)
            {
                foreach (GameObject i in brokenPiece)
                {
                    i.SetActive(true);
                    Destroy(i, 0.1f);
                    isbroken = true;
                    gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Player"))
        {
            //carrentHP--;
            carrentHP -= (other.gameObject.GetComponent<Player>().power + (int)other.gameObject.GetComponent<Player>().mass/(50 * 2));
        }
    }
}
