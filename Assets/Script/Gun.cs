using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    [SerializeField] protected Bullet bullet;
    protected Bullet b;
    [SerializeField] protected BoxCollider bc;
    protected RaycastHit hit;
    protected Vector3 aimPoint;
    protected float shotTimer = 0.0f;
    protected float shotTime = 0.4f;

    float rotationSpeed = 50;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bc=gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = true;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        shotTimer += Time.deltaTime;

        aimPoint = transform.forward;

        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public virtual void Fire(Gun gun)
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
                    b.SetDirection(direction2, gun);
                    shotTimer = 0.0f;
                }
            }
            BulletHit();
            
        }
    }

    void BulletHit()
    {
        Debug.Log("�e���u" + hit.collider.gameObject.name + "�v�Ƀq�b�g���܂����B");
    }
}
