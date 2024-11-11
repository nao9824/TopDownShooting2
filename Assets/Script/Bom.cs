using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    [SerializeField] Exp exp;

    public void BomExp()
    {
        Instantiate(exp, transform.position, Quaternion.identity);
    }
}
