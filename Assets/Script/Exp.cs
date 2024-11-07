using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.CompareTag("Bom"))
        {
            other.GetComponent<Bom>().BomExp();
        }
    }
}
