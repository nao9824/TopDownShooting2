using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerHand hand;

    float speed = 5.0f;
    [SerializeField] Rigidbody rb;

    public bool isHaveGun = false;
    public bool isShot = false;
    Vector3 clickPos = Vector3.zero;

    Vector3 direction;

    public bool isGoal;
    public bool isHave;

    [SerializeField] LayerMask layer;

    //�}�E�X�̕���������
    [SerializeField] UnityEngine.Camera mainCamera;
    Plane plane = new Plane();

    public Vector3 lookPoint;

    [SerializeField] LayerMask rayhitLayer;


    // Update is called once per frame
    void Update()
    {

        Move();
        Direction();

        if (isShot)
        {
            hand.Shot(clickPos, transform.forward);
            isShot = false;
        }
    }

    private void Move()
    {
        direction = Vector3.zero;

        if (isGoal)
        {
            speed = 1.0f;
            direction.z = speed;
        }
        else
        {
            direction.z = Input.GetAxis("Vertical");
            direction.x = Input.GetAxis("Horizontal");
        }
        rb.velocity = direction * speed;
    }

    //�}�E�X�̕�������
    void Direction()
    {
        // �J�����ƃ}�E�X�̈ʒu������Ray������
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        // �v���C���[�̍�����Plane���X�V���āA�J�����̏������ɒn�ʔ��肵�ċ������擾
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (Physics.Raycast(ray, out raycastHit,100, rayhitLayer))
        {

            // ���������Ɍ�_���Z�o���āA��_�̕�������
            lookPoint = raycastHit.point;

            if ((hand.transform.childCount > 0))
            {

                float h = hand.transform.GetChild(0).transform.position.y - lookPoint.y;
                float raylength = ray.direction.magnitude;
                float theta = Mathf.Acos(Vector3.Dot(-ray.direction, Vector3.up));
                float cosTheta = Mathf.Cos(theta);
                float S = h / cosTheta;
                Vector3 point = lookPoint + (-ray.direction) * S;
                hand.transform.LookAt(point);
            }
            lookPoint.y = 1.0f;
            transform.LookAt(lookPoint);
        }
    }

    public void ShotSetUp(Vector3 clickPosition)
    {
        clickPos = clickPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            hand.SetParent(collision.gameObject);
            isHave = true;
            collision.gameObject.layer = layer;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            isGoal = true;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
