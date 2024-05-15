using UnityEngine;
using System.Collections;

public class CatBartenderMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f; // �������� ��������
    public float moveDistance = 0.1f; // ����������, �� ������� ����� ��������� ����� � ����
    private Vector3 startPosition; // ��������� ������� ����-�������

    void Start()
    {
        startPosition = transform.position; // �������� ��������� �������
        StartCoroutine(MoveUpDown()); // ������ �������� ��� ��������
    }

    IEnumerator MoveUpDown()
    {
        while (true)
        {
            // ������� ����-������� ����� �� �������� ����������
            while (transform.position.y < startPosition.y + moveDistance)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ������� ����-������� ���� �� �������� ����������
            while (transform.position.y > startPosition.y - moveDistance)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
