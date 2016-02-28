using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text winText;            //Store a reference to the UI Text component which will display the 'You win' message.

    public int count;              //Integer to store the number of pickups collected so far.
    public bool flag_Clear = false;//クリアフラグ。クリアしたら立てる。

    // Use this for initialization
    void Start () {

        //Initialize count to zero.
        count = 0;

        //Initialze winText to a blank string since we haven't won yet at beginning.
        winText.text = "";

        //Call our SetCountText function which will update the text with the current value for count.
        SetCountText();

    }
	
	// Update is called once per frame
	void Update () {

        //クリア時の処理
        if (!flag_Clear)
        {
            //クリア条件
            if(count >= 12)
            {
                flag_Clear = true;
            }
        }

	}

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    public void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        countText.text = "Count: " + count.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (flag_Clear)
            //... then set the text property of our winText object to "You win!"
            winText.text = "You win!";
    }


}
