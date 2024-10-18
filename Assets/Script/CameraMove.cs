using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用

    Vector3 mousePos;

    // Use this for initialization
    void Start()
    {

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        mousePos=Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        
        //新しいトランスフォームの値を代入する
        transform.position = player.transform.position + offset + ((mousePos-player.transform.position) / 2);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(worldPosition);
        }
    }
}
