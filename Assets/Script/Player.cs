using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerHand hand;

    float speed = 5.0f;
    [SerializeField] Rigidbody rb;

    [SerializeField]
    public bool isHaveGun = false;
    public bool isShot = false;
    Vector3 clickPos = Vector3.zero;

    [SerializeField]Vector3 direction;

    public bool isGoal;
    public bool isHave;

    //�}�E�X�̕���������
    [SerializeField] UnityEngine.Camera mainCamera;
    Plane plane = new Plane();
    float distance = 0;

    public Vector3 lookPoint;
    [SerializeField]Vector3 forward;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {

        forward = transform.forward;
        Move();
        Direction();

        if (isShot)
        {
            hand.Shot(clickPos,forward);
            isShot = false;
        }


    }

    private void Move()
    {
        direction = Vector3.zero;

        direction.z = Input.GetAxis("Vertical");
        direction.x = Input.GetAxis("Horizontal");

        
        rb.velocity = direction * speed;
    }

    //�}�E�X�̕�������
    void Direction()
    {
        // �J�����ƃ}�E�X�̈ʒu������Ray������
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


        // �v���C���[�̍�����Plane���X�V���āA�J�����̏������ɒn�ʔ��肵�ċ������擾
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {

            // ���������Ɍ�_���Z�o���āA��_�̕�������
            lookPoint = ray.GetPoint(distance);
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
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            isGoal = true;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
