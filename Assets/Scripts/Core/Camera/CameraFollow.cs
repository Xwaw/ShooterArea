using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSet = new Vector3(0, 0, -10);
    [SerializeField] private float followSpeed = 8f;
    [SerializeField] private float mouseOffsetFactor = 5f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    void LateUpdate()
    {
        FollowTarget(target, offSet);
    }

    private void FollowTarget(Transform cameraTarget, Vector3 cameraOffset = default(Vector3))
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        
        if (mouseScreenPos.x >= 0 && mouseScreenPos.x <= Screen.width &&
            mouseScreenPos.y >= 0 && mouseScreenPos.y <= Screen.height)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;

            Vector3 toMouse = (mouseWorldPos - cameraTarget.position) / mouseOffsetFactor;
            Vector3 targetPosition = cameraTarget.position + toMouse + cameraOffset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
        else
        {
            Vector3 targetPosition = cameraTarget.position + cameraOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
    }
}
