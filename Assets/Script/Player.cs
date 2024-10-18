using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5.0f;
    [SerializeField] Rigidbody rb;

    [SerializeField]
    public bool isHaveGun = false;

    Vector3 direction;

    public bool isGoal;

    //マウスの方向を見る
    [SerializeField] UnityEngine.Camera mainCamera;
    Plane plane = new Plane();
    float distance = 0;

    public Vector3 lookPoint;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {


        Move();
        Direction();
        

    }

    private void Move()
    {
        direction = Vector3.zero;

        direction.z = Input.GetAxis("Vertical");
        direction.x = Input.GetAxis("Horizontal");

        if (Input.GetMouseButtonDown(0) && isHaveGun)
        {
            Debug.Log("撃った");

        }

        rb.velocity = direction * speed;
    }

    void Direction()
    {
        // カメラとマウスの位置を元にRayを準備
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {

            // 距離を元に交点を算出して、交点の方を向く
            lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
            /*collision.gameObject.transform.localPosition = new Vector3(collision.gameObject.transform.localPosition.x, collision.gameObject.transform.localPosition.y + 0.5f, collision.gameObject.transform.localPosition.z);
            Quaternion GunRotation = collision.transform.rotation;
            GunRotation = new Quaternion(collision.transform.localRotation.x, collision.transform.rotation.y, collision.transform.rotation.z, collision.transform.rotation.w);
            collision.transform.rotation = GunRotation;
            isHaveGun = true;*/
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            isGoal = true;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
