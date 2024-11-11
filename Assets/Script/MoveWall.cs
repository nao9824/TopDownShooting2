using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    [SerializeField] private List<GameObject> switchs = new List<GameObject>();

    bool open = false;
    float downSpeed = 5.0f;
    
    // Update is called once per frame
    void Update()
    {

        for (int i=0;i < switchs.Count;i++)
        {
            if (switchs[i] != null)
            {
                open=false;
                break;
            }
            else
            {
                open = true;
                
            }
        }

        if (open)
        {
            Vector3 pos = transform.position;
            pos.y -= downSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }
}
