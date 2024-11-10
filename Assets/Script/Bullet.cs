using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer lineRenderer;
    [SerializeField] Enemy enemy;
    [SerializeField] Bom bom;
    Gun haveGun;
    float speed = 25.0f;
    Vector3 dir;
    bool hitDetected = false;//���e������
    public bool isExp = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer��������܂���I");
        }

        // LineRenderer�̏����ݒ�
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hitDetected)
        {
            rb.velocity = dir * speed;

            // LineRenderer�̈ʒu���X�V
            if (haveGun != null)
            {
                lineRenderer.SetPosition(0, haveGun.transform.position);
                lineRenderer.SetPosition(1, transform.position);
            }
        }
        else
        {
            Vector3 start = lineRenderer.GetPosition(0);
            Vector3 end = lineRenderer.GetPosition(1);
            float distance = Vector3.Distance(start, end);

            if (distance > 0.1f)
            {
                Vector3 newStart = Vector3.Lerp(start, end, Time.deltaTime * speed);
                lineRenderer.SetPosition(0, newStart);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDirection(Vector3 direction, Gun gun)
    {
        dir = direction;
        Debug.Log("Direction set to: " + direction);
        haveGun = gun;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("MoveWall") || other.CompareTag("Switch") || other.CompareTag("Enemy") || other.CompareTag("Bom"))
        {
            hitDetected = true;
            rb.velocity = Vector3.zero; // �e�̑��x���[���ɂ���

            if (other.CompareTag("Bom"))
            {
                isExp = true;
                bom.BomExp();
                Destroy(other.gameObject);  
            }

            if (other.CompareTag("Enemy"))
            {
                enemy = other.GetComponent<Enemy>();
                enemy.hp--;
                if (enemy.hp <= 0)
                {
                    enemy.isDead = true;
                }
            }
        }
    }
   
}
