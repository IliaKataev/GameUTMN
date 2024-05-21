using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ShakerManager : MonoBehaviour
{
    public Text shakerText;
    public int maxCapacity = 20;
    private int currentCapacity = 0;
    private List<string> currentIngredients = new();

    public OrderManager orderManager;

    void Start()
    {
        ResetShaker();
    }

    public void AddIngredient(string ingredient)
    {
        if (currentCapacity < maxCapacity)
        {
            currentIngredients.Add(ingredient);
            currentCapacity++;
            UpdateShakerText();
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeShaker());
    }

    private IEnumerator ShakeShaker()
    {
        // Assuming the shaker has a Transform component for rotation animation
        float shakeDuration = 1f;
        float shakeAngle = 15f;
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float zAngle = Mathf.Sin(elapsedTime * Mathf.PI * 4 / shakeDuration) * shakeAngle;
            transform.rotation = initialRotation * Quaternion.Euler(0, 0, zAngle);
            yield return null;
        }

        transform.rotation = initialRotation;

    }

    void UpdateShakerText()
    {
        shakerText.text = string.Join(", ", currentIngredients.ToArray());
    }


    public void ResetShaker()
    {
        currentCapacity = 0;
        currentIngredients.Clear();
        UpdateShakerText();
    }

    public void ServeDrink()
    {
        // Logic for serving the drink and checking if it matches the order
        if (orderManager.CheckOrder(currentIngredients))
        {
            // Success logic
            orderManager.CompleteOrder();
        }
        else
        {
            // Failure logic
            Debug.Log("Incorrect drink. Start again.");
            ResetShaker();
        }
    }
}
