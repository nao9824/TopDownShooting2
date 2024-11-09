using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player player;   //�v���C���[���i�[�p
    private Vector3 offset;      //���΋����擾�p

    [SerializeField] LayerMask groundLayer;

    Vector3 mousePos;

    //�V�F�C�N
    Vector3 cameraPosition;
    float shakeNumX;
    float shakeNumY;
    float shakeTimer;

    float max;
    float min;
    float maxTime;

    // Use this for initialization
    void Start()
    {

        // MainCamera(�������g)��player�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;


        cameraPosition = transform.position;
        shakeNumX = 0;
        shakeNumY = 0;


        max = 0;
        min = 0;
    }

    // Update is called once per frame
    void Update()
    {

        mousePos=Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        //�V�����g�����X�t�H�[���̒l��������
        if (Physics.Raycast(ray, out hit,Mathf.Infinity,groundLayer))
        {
            transform.position = player.transform.position + offset + ((hit.point - player.transform.position) / 10);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(worldPosition);
        }

        /*//�V�F�C�N
        shakeTimer -= Time.deltaTime;

        if (shakeTimer >= 0)
        {
            float t = shakeTimer / maxTime;

            shakeNumX = Random.Range(min, max) * t;
            shakeNumY = Random.Range(min, max) * t;


            transform.position = cameraPosition + new Vector3(shakeNumX, shakeNumY, 0);

        }

        if (shakeTimer <= 0)
        {

            transform.position = cameraPosition;

        }*/
    }

    public void ShakeStart(float shakeTimer, float max, float min)
    {
        this.shakeTimer = shakeTimer;
        maxTime = shakeTimer;
        this.max = max;
        this.min = min;
    }
}
