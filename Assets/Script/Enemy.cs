using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navmeshAgent = null;
    [SerializeField] private List<GameObject> target = new List<GameObject>();
    private int currentTargetIndex = 0; // 現在の目標インデックスを追加
    [SerializeField] public int hp = 30;
    public bool isDead = false;
    public float stoppingDistance = 0.5f; // 到達距離の設定

    Vector3 playerPos;
    Vector3 enemyPos;

    public Transform player; // プレイヤーのTransform
    public float viewDistance = 10.0f; // 敵の視界距離
    public float lookAroundSpeed = 2.0f; // 視線を移動する速度
    public float maxLookAngle = 45.0f; // 最大視線移動角度
    private bool isPlayerLost = false;
    private bool lookingRight = true; // 視線が右方向に向いているかどうか
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        if (target.Count > 0)
        {
            navmeshAgent.SetDestination(target[currentTargetIndex].transform.position);
        }

        originalRotation = transform.rotation;//元の回転を保存

        playerPos=player.position;
        enemyPos=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Destroy(gameObject);
            return;
        }

        Move();
        Find();
    }

    void Move()
    {
        if (target.Count == 0) return;

        // 到達距離を明示的にチェック
        if (!navmeshAgent.pathPending && navmeshAgent.remainingDistance <= stoppingDistance)
        {
            currentTargetIndex = (currentTargetIndex + 1) % target.Count;
            navmeshAgent.SetDestination(target[currentTargetIndex].transform.position);
        }

    }

    void Find()
    {
        playerPos = player.position;
        enemyPos = transform.position;
        Vector3 direction = (playerPos-enemyPos).normalized;//距離求めて正規化
        float dot = Vector3.Dot(transform.forward, direction);//内積取る
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;//角度求める

        //もしthetaが規定以上の角度だったらRay飛ばす
        if (theta <= 90)
        {
            Ray ray = new Ray(transform.position,direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("当たった");
                }
            }
        }

    }

    //見失った時あたりを見回す
    void LookAround()
    {
        float angle = maxLookAngle * Mathf.Sin(Time.time * lookAroundSpeed); 
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, angle, 0); 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lookAroundSpeed);
    
    }
}
