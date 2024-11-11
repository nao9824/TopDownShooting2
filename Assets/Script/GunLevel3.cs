using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunLevel3 : Gun
{

    [SerializeField] Grenade grenade;
    Grenade g;

    float grenadeShotTime = 0.5f;
    float grenadeShotTimer = 0.0f;

    protected override void Start()
    {
        
        // オーバーライドした shotTime の値を設定します
        shotTime = 0.3f; 
    }

    new void Update()
    {
        grenadeShotTimer += Time.deltaTime;
        base.Update();
    }


    public override void Fire(Gun gun)
    {
        Ray ray = new Ray(transform.position, aimPoint);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 direction2 = (hit.point - transform.position).normalized;
            if (shotTimer > shotTime)
            {
                if (gun != null)
                {
                    b = Instantiate(bullet, transform.position, Quaternion.identity);
                    b.SetDirection(transform.position, hit.point);
                    shotTimer = 0.0f;
                }
            }
            if (grenadeShotTimer > grenadeShotTime)
            {
                g = Instantiate(grenade, transform.position, Quaternion.identity);
                g.SetDirection(direction2);
                grenadeShotTimer = 0.0f;
            }
        }
    }

}
