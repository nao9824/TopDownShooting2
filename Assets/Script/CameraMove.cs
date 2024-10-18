using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player player;   //�v���C���[���i�[�p
    private Vector3 offset;      //���΋����擾�p

    Vector3 mousePos;

    // Use this for initialization
    void Start()
    {

        // MainCamera(�������g)��player�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        mousePos=Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        
        //�V�����g�����X�t�H�[���̒l��������
        transform.position = player.transform.position + offset + ((mousePos-player.transform.position) / 2);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(worldPosition);
        }
    }
}
