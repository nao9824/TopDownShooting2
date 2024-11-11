using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGun : Gun
{

    int bulletNum = 5; 
    float reloadDuration = 2.0f; // �����[�h����
    float reloadTimer = 0.0f;
    bool isReloading = false;

    protected override void Start()
    {
        // �I�[�o�[���C�h���� shotTime �̒l��ݒ肵�܂�
        shotTime = 0.3f; 
    }

    protected override void Update()
    {
        shotTimer += Time.deltaTime; if (isReloading)
        {
            reloadTimer += Time.deltaTime; if (reloadTimer >= reloadDuration)
            {
                bulletNum = 5; // �e�������Z�b�g
                isReloading = false;
                reloadTimer = 0.0f; // �����[�h�^�C�}�[�����Z�b�g
            }
        }
        aimPoint = transform.forward;
    }

    public override void Fire(Gun gun)
    {
        if (!isReloading && bulletNum > 0)
        {
            Ray ray = new Ray(transform.position, aimPoint); if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 direction2 = (hit.point - transform.position).normalized;
                if (shotTimer > shotTime)//�ˌ��Ԋu�̃^�C�}�[�`�F�b�N
                {
                    if (gun != null)
                    {
                        b = Instantiate(bullet, transform.position, Quaternion.identity);
                        b.SetDirection(direction2, gun);
                        shotTimer = 0.0f;
                        bulletNum--; // �e�������炷
                        if (bulletNum <= 0) {
                            isReloading = true; // �����[�h���J�n
                        }
                    }
                }
            }
        }
    }
}
