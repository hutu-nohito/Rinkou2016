using UnityEngine;
using System.Collections;

public class CompleteRotator : MonoBehaviour {

    public int rot_speed = 45;

	//Update is called every frame
	void Update () 
	{
		//Rotate thet transform of the game object this is attached to by 45 degrees, taking into account the time elapsed since last frame.
		transform.Rotate (new Vector3 (0, 0, rot_speed) * Time.deltaTime);
	}
}
