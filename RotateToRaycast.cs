using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToRaycast : MonoBehaviour
{
    [SerializeField] new private Camera camera;

    private void RotateCastPoint(GameObject obj)
    {
        obj.transform.rotation = camera.transform.rotation * Quaternion.Euler(-1.5f, 1.7f, 0f);
        
    }

    private void Update()
    {
        RotateCastPoint(gameObject);
    }
}
