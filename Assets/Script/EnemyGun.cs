using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGun : Gun
{
    int bulletNum = 5;
    float reloadDuration = 2.0f; // リロード時間
    float reloadTimer = 0.0f;
    bool isReloading = false;

    void Start()
    {
        // オーバーライドした shotTime の値を設定します
        shotTime = 0.1f; // 例: 0.1秒に設定
    }

    void Update()
    {
        // 親クラスの Update メソッドを呼び出す
        base.Update();

        // リロード中ならリロードタイマーを進める
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            Debug.Log("Reloading... " + reloadTimer);

            if (reloadTimer >= reloadDuration)
            {
                bulletNum = 5;
                isReloading = false;
                reloadTimer = 0.0f; // リロードタイマーをリセット
                Debug.Log("Reload complete");
            }
        }
    }

    public new void Fire(Gun gun)
    {
        // リロード中でない場合にのみ発砲
        if (!isReloading && bulletNum > 0)
        {
            // 親クラスの Fire メソッドを呼び出す
            base.Fire(gun);
            bulletNum--;
            Debug.Log("Bullet fired. Remaining bullets: " + bulletNum);

            if (bulletNum <= 0)
            {
                isReloading = true; // リロード開始
                Debug.Log("Starting reload...");
            }
        }
        else if (isReloading)
        {
            Debug.Log("Cannot fire. Reloading...");
        }
        else
        {
            Debug.Log("Cannot fire. No bullets left.");
        }
    }
}
