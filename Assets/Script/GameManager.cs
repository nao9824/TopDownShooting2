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

    Vector3 mousePos= Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePos=Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        //弾撃つ
        if (Input.GetMouseButton(0) &&
            !player.isShot)
        {
            //銃持ってなかったら早期リターン
            if (!player.isHave)
            {
                return;
            }
            player.isShot = true;
            player.ShotSetUp(worldPosition);
            
        }

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
