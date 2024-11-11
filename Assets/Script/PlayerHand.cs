using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerHand : MonoBehaviour
{
    Gun haveGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void SetParent(GameObject gun)
    {
        if (haveGun != null) {

            Destroy(haveGun);
            haveGun = null;

        }

        gun.gameObject.TryGetComponent<Gun>(out haveGun);
        haveGun.transform.position = transform.position;
        haveGun.transform.rotation = transform.rotation;
        haveGun.transform.parent = transform;
        
    }

    public void Shot(Vector3 clickPosition,Vector3 direction)
    {

        haveGun.Fire(haveGun);

    }
}
