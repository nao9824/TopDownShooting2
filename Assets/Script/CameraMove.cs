using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用

    [SerializeField] LayerMask groundLayer;

    Vector3 mousePos;

    //シェイク
    Vector3 cameraPosition;
    float shakeNumX;
    float shakeNumY;
    float shakeTimer;

    float max;
    float min;
    float maxTime;

    // Use this for initialization
    void Start()
    {

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position;


        cameraPosition = transform.position;
        shakeNumX = 0;
        shakeNumY = 0;


        max = 0;
        min = 0;
    }

    // Update is called once per frame
    void Update()
    {

        mousePos=Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        //新しいトランスフォームの値を代入する
        if (Physics.Raycast(ray, out hit,Mathf.Infinity,groundLayer))
        {
            transform.position = player.transform.position + offset + ((hit.point - player.transform.position) / 10);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(worldPosition);
        }

        /*//シェイク
        shakeTimer -= Time.deltaTime;

        if (shakeTimer >= 0)
        {
            float t = shakeTimer / maxTime;

            shakeNumX = Random.Range(min, max) * t;
            shakeNumY = Random.Range(min, max) * t;


            transform.position = cameraPosition + new Vector3(shakeNumX, shakeNumY, 0);

        }

        if (shakeTimer <= 0)
        {

            transform.position = cameraPosition;

        }*/
    }

    public void ShakeStart(float shakeTimer, float max, float min)
    {
        this.shakeTimer = shakeTimer;
        maxTime = shakeTimer;
        this.max = max;
        this.min = min;
    }
}
