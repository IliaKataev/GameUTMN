using UnityEngine;
using System.Collections;

public class CatBartenderMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f; // Скорость движения
    public float moveDistance = 0.1f; // Расстояние, на которое будет двигаться вверх и вниз
    private Vector3 startPosition; // Начальная позиция кота-бармена

    void Start()
    {
        startPosition = transform.position; // Запомним начальную позицию
        StartCoroutine(MoveUpDown()); // Начнем корутину для движения
    }

    IEnumerator MoveUpDown()
    {
        while (true)
        {
            // Двигаем кота-бармена вверх на заданное расстояние
            while (transform.position.y < startPosition.y + moveDistance)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Двигаем кота-бармена вниз на заданное расстояние
            while (transform.position.y > startPosition.y - moveDistance)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
