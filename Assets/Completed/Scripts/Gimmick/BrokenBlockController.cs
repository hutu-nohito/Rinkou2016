using UnityEngine;
using System.Collections;

public class BrokenBlockController : MonoBehaviour {

    public int maxHP = 5;//壊れるまでの回数
    private int carrentHP;
    private bool isbroken = false;

    public GameObject[] brokenPiece;//壊れた時に飛ぶ破片

    //SE
    AudioSource audio_source;

    [SerializeField]
    AudioClip SE;

	// Use this for initialization
	void Start () {

        audio_source = GetComponent<AudioSource>();
        carrentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(carrentHP <= 0)
        {
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            if (!isbroken)
            {
                foreach (GameObject i in brokenPiece)
                {
                    i.SetActive(true);
                    Destroy(i, 0.1f);
                    isbroken = true;
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
            audio_source.PlayOneShot(SE);
            StartCoroutine(Crumble());
        }
    }

    //プルプルさせる
    IEnumerator Crumble()
    {
        Vector2 OldPos = transform.parent.transform.position;

        for (int i = 0;i < 10; i++)
        {
            transform.parent.transform.position += new Vector3(Random.Range(-0.3f,0.3f), Random.Range(-0.3f, 0.3f),0);

            yield return new WaitForSeconds(0.02f);

            transform.parent.transform.position = OldPos;

            yield return new WaitForSeconds(0.02f);
        }

    }
}
