using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGun : Gun
{
    
    void Start()
    {
        // オーバーライドした shotTime の値を設定します
        shotTime = 0.3f; 
    }

    
    /*public override void Fire(Gun gun)
    {
        
    }*/
}
