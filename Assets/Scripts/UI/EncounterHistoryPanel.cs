using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterHistoryPanel : MonoBehaviour
{
    public static EncounterHistoryPanel Instance { get; private set; }

    public ScrollRect scrollRect;
    public TextMeshProUGUI encounterTextElementPrefab;
    public Transform contentTransform;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddEntry(TrackEncounter encounter)
    {
        TextMeshProUGUI element = SpawnElement();
        element.text = $"Starting encounter: {encounter.type} - {encounter.description}";
    }

    public void AddEntry(string content)
    {
        TextMeshProUGUI element = SpawnElement();
        element.text = content;
    }

    private TextMeshProUGUI SpawnElement()
    {
        TextMeshProUGUI element = Instantiate(encounterTextElementPrefab, contentTransform);

        // scroll to bottom
        Canvas.ForceUpdateCanvases(); // hacky
        scrollRect.verticalNormalizedPosition = 0;

        return element;
    }
}
