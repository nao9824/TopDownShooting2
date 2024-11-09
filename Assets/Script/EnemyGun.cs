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

    void Start()
    {
        // �I�[�o�[���C�h���� shotTime �̒l��ݒ肵�܂�
        shotTime = 0.1f; // ��: 0.1�b�ɐݒ�
    }

    void Update()
    {
        // �e�N���X�� Update ���\�b�h���Ăяo��
        base.Update();

        // �����[�h���Ȃ烊���[�h�^�C�}�[��i�߂�
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            Debug.Log("Reloading... " + reloadTimer);

            if (reloadTimer >= reloadDuration)
            {
                bulletNum = 5;
                isReloading = false;
                reloadTimer = 0.0f; // �����[�h�^�C�}�[�����Z�b�g
                Debug.Log("Reload complete");
            }
        }
    }

    public new void Fire(Gun gun)
    {
        // �����[�h���łȂ��ꍇ�ɂ̂ݔ��C
        if (!isReloading && bulletNum > 0)
        {
            // �e�N���X�� Fire ���\�b�h���Ăяo��
            base.Fire(gun);
            bulletNum--;
            Debug.Log("Bullet fired. Remaining bullets: " + bulletNum);

            if (bulletNum <= 0)
            {
                isReloading = true; // �����[�h�J�n
                Debug.Log("Starting reload...");
            }
        }
        else if (isReloading)
        {
            Debug.Log("Cannot fire. Reloading...");
        }
        else
        {
            Debug.Log("Cannot fire. No bullets left.");
        }
    }
}
