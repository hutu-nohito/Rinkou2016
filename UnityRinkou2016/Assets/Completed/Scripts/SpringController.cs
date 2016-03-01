using UnityEngine;
using System.Collections;

public class SpringController : MonoBehaviour {

    public float power = 10;//ばねの弾性力

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 ForceDirection = (other.gameObject.transform.position - transform.position).normalized;//ばねからプレイヤー方向

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(ForceDirection * power,ForceMode2D.Impulse);
        }
    }
}
