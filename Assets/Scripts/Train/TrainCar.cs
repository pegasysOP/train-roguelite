using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class TrainCar : MonoBehaviour
{
    public string carName;
    [TextArea(2, 5)]
    public string carDescription;
    public int currentHealth = 2; // 2: full, 1: damaged, 0: destroyed

    [Header("Visual")]
    public GameObject damageIndicator;

    public bool isDamaged => currentHealth <= 1;
    public bool isDestroyed => currentHealth <= 0;

    private Vector3 offset;
    private bool dragging = false;

    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
        worldMouse.z = 0;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldMouse, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                dragging = true;

                // show above
                transform.position -= new Vector3(0, 0, 1);
                transform.localScale = Vector3.one * 1.2f;

                offset = transform.position - worldMouse;
            }
        }

        if (dragging && Mouse.current.leftButton.isPressed)
        {
            transform.position = worldMouse + offset;
        }

        if (dragging && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragging = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            transform.localScale = Vector3.one;

            // Snap to closest index
            FindFirstObjectByType<TrainController>().SnapCarToNearestSlot(this);
        }
    }

    public virtual void OnHazard()
    {
    }

    public virtual void OnLoot()
    {
    }

    public virtual void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered");
    }

    public virtual void Damage()
    {
        currentHealth--;

        DoPunchAnimation();
        
        if (isDestroyed)
        {
            EncounterHistoryPanel.Instance.AddEntry($"{carName} is destroyed!");
            gameObject.SetActive(false);
        }
        else if (isDamaged)
        {
            EncounterHistoryPanel.Instance.AddEntry($"{carName} is damaged!");
            damageIndicator.SetActive(true);
        }
    }

    public void DoPunchAnimation()
    {
        transform.DOPunchScale(Vector3.one / 2, 0.5f, 0, 0);
    }
}
