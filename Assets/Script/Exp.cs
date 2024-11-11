using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exp : MonoBehaviour
{
    CameraMove cameraMove;
    float expScale = 40.0f;
    float expScaleMax = 7.0f;

    //徐々に透明にする
    public float duration = 0.1f; // フェードアウトの時間
    private Material material;
    private Color initialColor;
    private float timer = 0.0f;
    private bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraMove = Camera.main.GetComponent<CameraMove>();
        material = GetComponent<Renderer>().material; 
        initialColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {

        Exposion();
        Fade();

        if (transform.localScale.x >= expScaleMax)
        {
            Destroy(gameObject);
        }
    }

    public void Exposion()
    {
        isFading = true;
        Vector3 scale;
        scale = transform.localScale;
        scale += new Vector3(expScale * Time.deltaTime, expScale * Time.deltaTime, expScale * Time.deltaTime);
        transform.localScale = scale;
        cameraMove.ShakeStart(0.1f, 0.1f, -0.1f);

    }

    void Fade()
    {
        if (isFading)
        { // フェードアウト処理
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, timer / duration);
            // 新しいカラーを設定
            material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            // フェードアウトが完了したらフェードを停止
            if (timer >= duration)
            {
                isFading = false; timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            return;
        }

        Vector3 hitPos=other.transform.position;
        Vector3 expPos = transform.position;
        Vector3 direction = (hitPos - expPos).normalized;//距離求めて正規化

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            Debug.Log("爆発が「" + hit.collider.gameObject.name + "」にヒットしました。");

            if (hit.collider.CompareTag("Bom"))
            {
                hit.collider.gameObject.GetComponent<Bom>().BomExp();
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().hp -= hit.collider.gameObject.GetComponent<Enemy>().hpMax;
            }
        }

    }
}
