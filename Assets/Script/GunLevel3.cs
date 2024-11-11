using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevel3 : Gun
{

    protected override void Start()
    {
        bc = gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = true;

        // オーバーライドした shotTime の値を設定します
        shotTime = 0.2f; 
    }



}
