using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevel2 : Gun
{

    protected override void Start()
    {
        
        // オーバーライドした shotTime の値を設定します
        shotTime = 0.3f; 
    }

}
