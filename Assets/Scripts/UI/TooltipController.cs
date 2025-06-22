using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipController : MonoBehaviour
{
    public static TooltipController Instance;

    public RectTransform panelRect;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI bodyText;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
        worldMouse.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(worldMouse, Vector2.zero);
        if (hit.collider != null)
        {
            TrainCar car = hit.collider.GetComponent<TrainCar>();
            if (car != null)
            {
                Show(car.carName, car.carDescription);
                panelRect.position = mousePos + new Vector2(0, -10); // small offset
                return;
            }

            TrainEngine engine = hit.collider.GetComponent<TrainEngine>();
            if (engine != null)
            {
                Show("Train Engine", $"Provides enough power to pull {FindFirstObjectByType<TrainController>().enginePower} cars");
                panelRect.position = mousePos + new Vector2(0, -10); // small offset
                return;
            }
        }

        Hide();
    }


    public void Show(string name, string description)
    {
        panelRect.gameObject.SetActive(true);
        headerText.text = name;
        bodyText.text = description;
    }

    public void Hide()
    {
        panelRect.gameObject.SetActive(false);
    }
}
