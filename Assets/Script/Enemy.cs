using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navmeshAgent = null;
    [SerializeField] private List<GameObject> target = new List<GameObject>();
    private int currentTargetIndex = 0; // ���݂̖ڕW�C���f�b�N�X��ǉ�
    [SerializeField] public int hp = 30;
    public bool isDead = false;
    public float stoppingDistance = 0.5f; // ���B�����̐ݒ�

    Vector3 playerPos;
    Vector3 enemyPos;

    public Transform player; // �v���C���[��Transform
    public float viewDistance = 10.0f; // �G�̎��E����
    public float lookAroundSpeed = 2.0f; // �������ړ����鑬�x
    public float maxLookAngle = 45.0f; // �ő压���ړ��p�x
    private bool isPlayerLost = false;
    private bool lookingRight = true; // �������E�����Ɍ����Ă��邩�ǂ���
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        if (target.Count > 0)
        {
            navmeshAgent.SetDestination(target[currentTargetIndex].transform.position);
        }

        originalRotation = transform.rotation;//���̉�]��ۑ�

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

        // ���B�����𖾎��I�Ƀ`�F�b�N
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
        Vector3 direction = (playerPos-enemyPos).normalized;//�������߂Đ��K��
        float dot = Vector3.Dot(transform.forward, direction);//���ώ��
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;//�p�x���߂�

        //����theta���K��ȏ�̊p�x��������Ray��΂�
        if (theta <= 90)
        {
            Ray ray = new Ray(transform.position,direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("��������");
                }
            }
        }

    }

    //���������������������
    void LookAround()
    {
        float angle = maxLookAngle * Mathf.Sin(Time.time * lookAroundSpeed); 
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, angle, 0); 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lookAroundSpeed);
    
    }
}
