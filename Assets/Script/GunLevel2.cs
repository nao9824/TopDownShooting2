using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevel2 : Gun
{

    void Start()
    {
        // オーバーライドした shotTime の値を設定します
        shotTime = 0.1f; // 例: 0.1秒に設定
    }

}
