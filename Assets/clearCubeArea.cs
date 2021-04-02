using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will clear close proximity of the realmCube
/// </summary>
public class clearCubeArea : MonoBehaviour
{
    void Start()
    {
        Collider[] nearObjects = Physics.OverlapSphere(transform.position, 50);
        foreach (var item in nearObjects)
        {
            if (item.tag == "asteroid")
            {
                Destroy(item.gameObject);
            }
        }
    }
}
