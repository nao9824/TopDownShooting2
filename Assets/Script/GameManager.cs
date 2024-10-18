using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    [SerializeField] Player player;

    //フェードアウト
    [SerializeField] Image fadePanel;
    float fadeDuration = 0.8f;

    //リザルト
    [SerializeField] Canvas resultPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player.isGoal)
        {
            FadeOut();
        }

    }

    void FadeOut()
    {
        //フェードアウト開始
        fadePanel.color = Color.Lerp(fadePanel.color, new Color(1, 1, 1f, 1), fadeDuration * Time.deltaTime);
        if (fadePanel.color == new Color(1, 1, 1f, 1))
        {
            resultPanel.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
