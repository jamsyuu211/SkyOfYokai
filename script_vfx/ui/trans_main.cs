using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class trans_main : MonoBehaviour
{
    public SpriteRenderer fadeImage;
    public float fadeSpeed = 1.5f;
    float t = 0f;
    bool is_trans_main = false;
    bool update_t = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (update_t)
        {
            if (t > 3f)
            {
                update_t = false;
                t = 0f;
                is_trans_main = true;
            }
        }
        
        if(is_trans_main )
        {
            is_trans_main = false;
            FadeToScene("plane_shooting");
        }
    }
    //자연스러운 씬 전환을 위한 함수들
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeIn(sceneName));
    }

    IEnumerator FadeIn(string sceneName)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0f, 0f, 0f, t);
            yield return null;
        }

        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator FadeOut()
    {
        float t = 1.0f;
        while (t > 0.0f)
        {
            t -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0f, 0f, 0f, t);
            yield return null;
        }
    }
}
