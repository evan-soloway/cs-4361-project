using UnityEngine;
using TMPro;  // Add this line

public class inventoryupdate : MonoBehaviour
{
    // Maximum number of goblets that can be collected
    public  int maxGoblets = 5;

    // Current number of goblets collected
    public int currentGoblets = 0;

    // UI Text component
    private TextMeshProUGUI inventoryText;  // Change this line

    void Start()
    {
        // Get the TextMeshProUGUI component
        inventoryText = GetComponent<TextMeshProUGUI>();  // And this line

        // Initialize the text
        UpdateInventoryText();
    }

    // Call this method when a goblet is collected
    public void OnGobletCollected()
    {
        // Increment the current number of goblets
        currentGoblets++;

        // Make sure it doesn't exceed the maximum
        currentGoblets = Mathf.Min(currentGoblets, maxGoblets);

        // Update the text
        UpdateInventoryText();
    }

    // Updates the inventory text
    private void UpdateInventoryText()
    {
        inventoryText.text = currentGoblets + "/" + maxGoblets + " Goblets Collected";
    }
}
