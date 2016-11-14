using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Player"))
        {
            Variable.playstate = Utility.PlayState.Clear;
        }
    }
}
