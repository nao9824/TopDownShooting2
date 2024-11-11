using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    [SerializeField] protected Bullet bullet;
    protected Bullet b;
    protected RaycastHit hit;
    protected Vector3 aimPoint;
    protected float shotTimer = 0.0f;
    protected float shotTime = 0.6f;

    float rotationSpeed = 50;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        shotTimer += Time.deltaTime;

        aimPoint = transform.forward;

        if (transform.parent == null)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    public virtual void Fire(Gun gun)
    {
        if (shotTimer > shotTime)
        {
            Ray ray = new Ray(transform.position, aimPoint);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 direction2 = (hit.point - transform.position).normalized;
                
                if (gun != null)
                {
                    b = Instantiate(bullet, transform.position, Quaternion.identity);
                    b.SetDirection(transform.position, hit.point);
                    shotTimer = 0.0f;
                }

                BulletHit();

                if (hit.collider.CompareTag("Switch"))
                {
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<Enemy>().hp--;
                }
                if (hit.collider.CompareTag("Bom"))
                {
                    hit.collider.gameObject.GetComponent<Bom>().BomExp();
                    Destroy(hit.collider.gameObject);

                }
            }
        }
    }

    void BulletHit()
    {
        Debug.Log("弾が「" + hit.collider.gameObject.name + "」にヒットしました。");
    }
}
