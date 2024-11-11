using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] Exp exp;
    Rigidbody rb;

    float speed = 5.0f;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = dir * speed;

    }

    void BomExp()
    {
        Instantiate(exp, transform.position, Quaternion.identity);
    }

    public void SetDirection(Vector3 direction)
    {
        dir = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Wall") ||
            other.gameObject.CompareTag("Switch") ||
            other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Bom"))
        {
            BomExp();
            Destroy(gameObject);
        }
    }
}
