using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyGun enemyGun;

    NavMeshAgent navmeshAgent = null;
    [SerializeField] private List<GameObject> target = new List<GameObject>();
    private int currentTargetIndex = 0; // 現在の目標インデックスを追加
    public int hp;
    public int hpMax = 5;
    public bool isDead = false;
    public float stoppingDistance = 0.5f; // 到達距離の設定

    Vector3 playerPos;
    Vector3 enemyPos;
    float findAngle = 90;
    float rotationSpeed = 10.0f;
    
    public Transform player; // プレイヤーのTransform
    public float viewDistance = 10.0f; // 敵の視界距離
    public float lookAroundSpeed = 2.0f; // 視線を移動する速度
    public float maxLookAngle = 45.0f; // 最大視線移動角度
    private bool isPlayerLost = false;
    private bool lookingRight = true; // 視線が右方向に向いているかどうか
    private Quaternion originalRotation;
    float rotateTime = 3.0f;
    float rotateTimer;

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
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            isDead = true;
        }
        if (isDead)
        {
            Destroy(gameObject);
        }
        else
        {
            
            if (isPlayerLost)
            {
                LookAround();
            }

            if (!isPlayerLost)
            {
                Move();

            }
            Find();
        }
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
        if (theta <= findAngle)
        {
            Ray ray = new Ray(transform.position,direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, ray.direction);
                if (hit.collider.CompareTag("Player"))
                {
                    rotateTimer = rotateTime;
                    isPlayerLost = false;

                    // 敵をプレイヤーの方向に向ける
                    Vector3 lookDirection = (player.position - enemyGun.transform.position).normalized; 
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection); 
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                    //Moveを止める
                    if (!isDead)
                    {
                        navmeshAgent.isStopped = true;

                        enemyGun.Fire(enemyGun);
                    }
                }
                else
                {
                    if (rotateTimer > 0.0f)
                    {
                        isPlayerLost = true;
                        originalRotation = transform.rotation;//元の回転を保存
                        
                    }
                }
            }
        }

    }

    //見失った時あたりを見回す
    void LookAround()
    {
        rotateTimer -= Time.deltaTime;

        float angle = maxLookAngle * Mathf.Sin(Time.time * lookAroundSpeed); 
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, angle, 0); 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lookAroundSpeed);

        if (rotateTimer <= 0.0f)
        {
            isPlayerLost = false;
            navmeshAgent.isStopped = false;
            
        }
    }
}
