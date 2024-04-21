using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class start_button : MonoBehaviour
{
    private AudioSource sound;
    private Camera main_camera;
    Color push_color = new Color(0.8f, 0.8f, 0.8f, 1f);
    private SpriteRenderer sr_color;
    public SpriteRenderer fadeImage;
    public float fadeSpeed = 1.5f;
    Vector3 mouse_pos;
    bool is_changed = false;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sr_color = GetComponent<SpriteRenderer>();
        main_camera = GetComponent<Camera>();
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
                if ((mouse_pos.y <= -2.5 && mouse_pos.y >= -3.25) && (mouse_pos.x >= -1 && mouse_pos.x <= 1))
                {
                    is_changed = true;
                    sound.mute = false;
                    sound.Play();
                    sr_color.color = push_color;
                    FadeToScene("loading");
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
