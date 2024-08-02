using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarRotation : MonoBehaviour
{
    public string cameraName = "Camera"; // ã�ƾ� �� ī�޶��� �̸�

    private Camera targetCamera;

    void Start()
    {
        // ������ �̸��� ���� ī�޶� ã���ϴ�.
        GameObject cameraObject = GameObject.Find(cameraName);
        if (cameraObject != null)
        {
            targetCamera = cameraObject.GetComponent<Camera>();
        }
        else
        {
            Debug.LogError("ī�޶� ã�� �� �����ϴ�: " + cameraName);
        }
    }

    void Update()
    {
        if (targetCamera != null)
        {
            // �� ������Ʈ�� ������ ī�޶� �������� ȸ����ŵ�ϴ�.
            transform.LookAt(targetCamera.transform);
        }
    }
}