using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float cameraAngleX = 60f;  // X�� ȸ�� ����
    [Range(5f, 20f)] public float cameraDistance = 11.18f;  // ī�޶�� ��ǥ ������ �Ÿ�
    [SerializeField] private Vector3 targetPosition = Vector3.zero;  // �ٶ󺸴� ��ǥ ��ġ

    private void Start()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        // �������� ��ȯ
        float angleInRadian = cameraAngleX * Mathf.Deg2Rad;

        // ������ �Ÿ��� �̿��� Y�� Z ��ġ ���
        float yOffset = cameraDistance * Mathf.Sin(angleInRadian);
        float zOffset = -cameraDistance * Mathf.Cos(angleInRadian);

        // ī�޶� ��ġ ����
        transform.position = new Vector3(targetPosition.x, yOffset, targetPosition.z + zOffset);

        // ī�޶� ��ǥ���� �ٶ󺸵��� ȸ��
        transform.rotation = Quaternion.Euler(cameraAngleX, 0f, 0f);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateCameraPosition();
    }
#endif
}
