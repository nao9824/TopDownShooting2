using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevel3 : Gun
{

    protected override void Start()
    {
        bc = gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = true;

        // �I�[�o�[���C�h���� shotTime �̒l��ݒ肵�܂�
        shotTime = 0.2f; 
    }



}
