using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditRandomizer : MonoBehaviour
{
    // List of names to display in the credits
    private List<string> names = new List<string>() { "Faye Tom", "Sjoerd Wouters", "Loek Ota" };

    // TextMeshPro component that displays the credits - assign this in the inspector
    public TextMeshProUGUI creditsText;

    private void Start()
    {
        RandomizeNames();
        DisplayCredits();
    }

    private void RandomizeNames()
    {
        // Shuffle the names using the Fisher-Yates shuffle algorithm
        for (int i = names.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string temp = names[i];
            names[i] = names[randomIndex];
            names[randomIndex] = temp;
        }
    }

    private void DisplayCredits()
    {
        // Initialize credits string with "Developed by "
        string creditsString = "Developed by ";

        // Concatenate all names except the last with a comma
        for (int i = 0; i < names.Count - 1; i++)
        {
            creditsString += names[i] + ", ";
        }

        // Remove the last comma and space, then add "and " before the last name
        creditsString = creditsString.TrimEnd(new char[] { ',', ' ' }) + " and " + names[names.Count - 1];

        // Set the text of the TextMeshPro component
        creditsText.text = creditsString;
    }
}
