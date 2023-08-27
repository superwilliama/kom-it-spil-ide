using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text clayText;

    public int currentWood;
    public int currentStone;
    public int currentClay;

    public void UpdateCurrentResourcesUI()
    {
        woodText.text = "Wood: " + currentWood;
        stoneText.text = "Stone: " + currentStone;
        clayText.text = "Clay: " + currentClay;
    }
}
