using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWallPos : MonoBehaviour
{
    Camera cam;
    float aspectRatio;
    BoxCollider levelWallCollider;
    
    void Start()
    {
        if (cam == null)
            cam = Camera.main;
        aspectRatio = cam.aspect;
        Vector3 farClipCenter;
        farClipCenter = cam.transform.position + cam.transform.forward * cam.farClipPlane;
        transform.position = farClipCenter;
        float colliderSizeY = cam.farClipPlane * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView);
        float colliderSizeX = aspectRatio * colliderSizeY;
        transform.localRotation = cam.transform.localRotation;
        levelWallCollider = gameObject.GetComponent<BoxCollider>();
        levelWallCollider.size = new Vector3 (colliderSizeX, colliderSizeY, 0f);
    }
}
