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

    protected override void Start()
    {
        // オーバーライドした shotTime の値を設定します
        shotTime = 0.3f; 
    }

    protected override void Update()
    {
        shotTimer += Time.deltaTime; if (isReloading)
        {
            reloadTimer += Time.deltaTime; if (reloadTimer >= reloadDuration)
            {
                bulletNum = 5; // 弾数をリセット
                isReloading = false;
                reloadTimer = 0.0f; // リロードタイマーをリセット
            }
        }
        aimPoint = transform.forward;
    }

    public override void Fire(Gun gun)
    {
        if (!isReloading && bulletNum > 0)
        {
            Ray ray = new Ray(transform.position, aimPoint); if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 direction2 = (hit.point - transform.position).normalized;
                if (shotTimer > shotTime)//射撃間隔のタイマーチェック
                {
                    if (gun != null)
                    {
                        b = Instantiate(bullet, transform.position, Quaternion.identity);
                        b.SetDirection(direction2, gun);
                        shotTimer = 0.0f;
                        bulletNum--; // 弾数を減らす
                        if (bulletNum <= 0) {
                            isReloading = true; // リロードを開始
                        }
                    }
                }
            }
        }
    }
}
