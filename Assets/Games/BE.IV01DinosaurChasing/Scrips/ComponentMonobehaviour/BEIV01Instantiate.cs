using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01Instantiate : MonoBehaviour
{
    public GameObject InstantiatePrefab(GameObject prefab, Transform transform)
    {
        return Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
