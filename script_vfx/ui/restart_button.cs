using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_button : MonoBehaviour
{
    private Camera main_camera;
    Color push_color = new Color(0.8f, 0.8f, 0.8f, 1f);
    private SpriteRenderer color;
    Vector3 mouse_pos;
    public GameObject ui_total_score;
    private ui_total_score total_score_script;

    //버튼 클릭 사운드 관련 변수
    private AudioSource sound;

    //자연스러운 씬전환관련 변수
    public SpriteRenderer fadeImage;
    public float fadeSpeed = 1.5f;
    bool is_changed = false;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        total_score_script = ui_total_score.GetComponent<ui_total_score>();
        color = GetComponent<SpriteRenderer>();
        main_camera = GetComponent<Camera>();
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        if (total_score_script.is_finished_print_score)
        {
            if (!is_changed) {
                if (Input.GetMouseButtonDown(0))
                {
                    mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if ((mouse_pos.y <= -3.2 && mouse_pos.y >= -3.8) && (mouse_pos.x >= -0.92 && mouse_pos.x <= 0.97))
                    {
                        is_changed = true;
                        sound.mute = false;
                        sound.Play();
                        color.color = push_color;
                        total_score_script.is_finished_print_score = false;
                        FadeToScene("opening");
                    }
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
