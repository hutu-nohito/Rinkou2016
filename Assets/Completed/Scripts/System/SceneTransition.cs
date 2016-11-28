using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private FadeImage Fade_Image;

    Sound_Controller SC;

    private string SceneName = "Home";
    private bool isCoroutine;
    
    //フェード用
    private float elapsedTime = 0;
    private bool isFade = false;
    private bool flag_fade = false;
    [SerializeField]
    private float fade_time = 1;//フェードにかける時間

    //時間を止めてても動くように(どこでも使えるようにしとくと便利かも)
    //http://unity-michi.com/post-411/
    float realDeltaTime;
    float lastRealTime;
    //現実時間基準でデルタ時間を求める.
    void CalcRealDeltaTime()
    {
        if (lastRealTime == 0)
        {
            lastRealTime = Time.realtimeSinceStartup;
        }
        realDeltaTime = Time.realtimeSinceStartup - lastRealTime;
        lastRealTime = Time.realtimeSinceStartup;

    }
    
    void Start()
    {
        SC = GetComponent<Sound_Controller>();
    }

    void Update()
    {
        CalcRealDeltaTime();

        if (isFade)
        {
            //フェード
            if (flag_fade)
            {
                elapsedTime += realDeltaTime;
                Fade_Image.Range = elapsedTime / fade_time;
                if (elapsedTime > fade_time)
                {
                    Fade_Image.Range = 1;
                    elapsedTime = 0;
                    isFade = false;

                }
            }

            //フェード解除
            if (!flag_fade)
            {
                elapsedTime += realDeltaTime;
                Fade_Image.Range = 1 - (elapsedTime / fade_time);
                if (elapsedTime > fade_time)
                {
                    Fade_Image.Range = 0;
                    elapsedTime = 0;
                    isFade = false;
                }
            }
        }
        
    }

    //場面転換用フェード
    public void Fade(bool fade)
    {
        isFade = true;
        flag_fade = fade;
    }

    public void SceneSet(string SceneName)
    {
        this.SceneName = SceneName;
        StartCoroutine(TransScene());
    }

    //場面転換
    private AsyncOperation Asy = null;
    private IEnumerator TransScene()
    {
        if (isCoroutine) yield break;
        isCoroutine = true;
        Fade(true);

        //マシンパラメタの引継ぎ


        Asy = SceneManager.LoadSceneAsync(SceneName);

        Asy.allowSceneActivation = false;

        yield return new WaitForSeconds(fade_time + 1);

        Asy.allowSceneActivation = true;

        SC.BGM(SceneName);//BGMを変える

        Fade(false);

        Variable.playstate = Utility.PlayState.Start;

        isCoroutine = false;
    }

}