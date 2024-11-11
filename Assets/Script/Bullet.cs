using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] Enemy enemy;
    [SerializeField] Bom bom;
    Gun haveGun;

    float max = 0.1f;
    Vector3 start;
    Vector3 end;
    float timer;
    float minLine = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñI");
        }

        // LineRenderer‚Ì‰Šúİ’è
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        start = lineRenderer.GetPosition(0);
        end = lineRenderer.GetPosition(1);
        float distance = Vector3.Distance(start, end);
        float t = timer / max;

        if (distance > minLine)
        {
            Vector3 newStart = Vector3.Lerp(start, end, t);
            lineRenderer.SetPosition(0, newStart);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SetDirection(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

    }

}
