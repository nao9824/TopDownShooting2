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

    //マウスの方向を見る
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

    //マウスの方向向く
    void Direction()
    {
        // カメラとマウスの位置を元にRayを準備
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (Physics.Raycast(ray, out raycastHit,100, rayhitLayer))
        {

            // 距離を元に交点を算出して、交点の方を向く
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
