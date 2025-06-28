using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrainController))]
public class TrainDragController : MonoBehaviour
{
    public TrainController trainController;

    private TrainCar hoveredCar;
    private TrainCar draggingCar;
    private Vector3 dragOffset;

    private void Update()
    {
        if (!trainController.AllowRearrangement)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
        worldMouse.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(worldMouse, Vector2.zero);
        hoveredCar = hit.collider != null ? hit.collider.GetComponent<TrainCar>() : null;

        // begin dragging
        if (Mouse.current.leftButton.wasPressedThisFrame && hoveredCar != null)
        {
            draggingCar = hoveredCar;

            draggingCar.transform.localScale = Vector3.one * 1.2f;
            draggingCar.transform.position -= new Vector3(0, 0, 1);

            dragOffset = draggingCar.transform.position - worldMouse;

            draggingCar.energyDisplay.gameObject.SetActive(false);
        }

        // dragging
        if (draggingCar != null && Mouse.current.leftButton.isPressed)
        {
            draggingCar.transform.position = worldMouse + dragOffset;
        }

        // end dragging
        if (draggingCar != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            draggingCar.transform.localScale = Vector3.one;
            draggingCar.transform.position += new Vector3(0, 0, 1);

            trainController.SnapCarToNearestSlot(draggingCar);

            draggingCar.energyDisplay.gameObject.SetActive(true);

            draggingCar = null;
        }
    }
}
