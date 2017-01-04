using UnityEngine;
using System.Collections;

public class SpringController : MonoBehaviour {

    public float power = 5;//ばねの弾性力
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 ForceDirection = (other.gameObject.transform.position - transform.position);//ばねからプレイヤー方向
            Vector2 PlayerVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(ForceDirection * power * other.gameObject.GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);

            //入射したとき速度が大きいほうはそのまま
            //if (PlayerVelocity.x > PlayerVelocity.y)//横に入射したとき
            //{
            //    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ForceDirection.x, 0) * power, ForceMode2D.Impulse);
            //}
            //else//縦
            //{
            //    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, ForceDirection.y) * power, ForceMode2D.Impulse);
            //}
        }
    }
}
