using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Enemy enemy;
    Bullet b;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 clickPosition ,Vector3 direction,Gun gun)
    {
        Ray ray = Camera.main.ScreenPointToRay(clickPosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 direction2 = (hit.point - transform.position).normalized;
            b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.SetDirection(direction2,gun);
            BulletHit();
            if(hit.collider.gameObject.name == "Enemy")
            {
                enemy.hp--;
                if (enemy.hp <= 0)
                {
                    enemy.isDead = true;
                }
            }

            if (b.isExp)
            {

            }
        }
       
    }

    void BulletHit()
    {
        Debug.Log("弾が「" + hit.collider.gameObject.name + "」にヒットしました。");
    }

}
