using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class change_home : MonoBehaviour
{
    Vector2 mouse_pos;
    private AudioSource sound;
    public SpriteRenderer fadeImage;
    public float fadeSpeed = 1.5f;
    bool is_changed = false;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_changed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (mouse_pos.x > -2.1f && mouse_pos.x < -1.35f && mouse_pos.y > -4.88f && mouse_pos.y < -3.95)
                {
                    is_changed = true;
                    sound.mute = false;
                    sound.Play();
                    FadeToScene("opening");
                }
            }
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
