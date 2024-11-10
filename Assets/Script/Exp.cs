using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Exposion();
        if (transform.localScale.x >= 5.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Exposion()
    {
        Vector3 scale;
        scale = transform.localScale;
        scale.x += 4.0f * Time.deltaTime;
        scale.y += 4.0f * Time.deltaTime;
        scale.z += 4.0f * Time.deltaTime;
        transform.localScale = scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPos=other.transform.position;
        Vector3 expPos = transform.position;
        Vector3 direction = (hitPos - expPos).normalized;//�������߂Đ��K��

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            Debug.Log("�������u" + hit.collider.gameObject.name + "�v�Ƀq�b�g���܂����B");

            if (hit.collider.CompareTag("Bom"))
            {
                
            }
            
        }

    }
}
