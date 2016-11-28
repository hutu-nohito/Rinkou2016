using UnityEngine;
using System.Collections;

public class Sound_Controller : MonoBehaviour {

    AudioSource[] audiosource;
    [SerializeField]
    AudioClip[] BGMs;
    [SerializeField]
    AudioClip[] Clips;
    /*
        0:ボタン

    */

	// Use this for initialization
	void Start () {

        audiosource = GetComponents<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void BGM(string SceneName)
    {
        switch (SceneName)
        {
            case "Title":
                audiosource[0].clip = BGMs[0];
                audiosource[0].Play();
                break;
            case "Home":
                audiosource[0].clip = BGMs[1];
                audiosource[0].Play();
                break;
            case "Main":
                audiosource[0].clip = BGMs[2];
                audiosource[0].Play();
                break;
        }
    }

    public void ButtonSE()
    {
        audiosource[1].PlayOneShot(Clips[0]);
    }

    public void PowerUpSE()
    {
        audiosource[1].PlayOneShot(Clips[1]);
    }

    public void KabeSE()
    {
        audiosource[1].PlayOneShot(Clips[2]);
    }
}
