using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    Bullet b;
    RaycastHit hit;
    protected Vector3 aimPoint;
    float shotTimer = 0.0f;
    protected float shotTime = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        // ����������
    }

    // Update is called once per frame
    public void Update()
    {
        shotTimer += Time.deltaTime;

        aimPoint = transform.forward;
    }

    public void Fire(Gun gun)
    {
        Ray ray = new Ray(transform.position, aimPoint);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 direction2 = (hit.point - transform.position).normalized;
            if (shotTimer > shotTime)
            {
                b = Instantiate(bullet, transform.position, Quaternion.identity);
                b.SetDirection(direction2, gun);
                shotTimer = 0.0f;
            }
            BulletHit();
            
        }
    }

    void BulletHit()
    {
        Debug.Log("�e���u" + hit.collider.gameObject.name + "�v�Ƀq�b�g���܂����B");
    }
}