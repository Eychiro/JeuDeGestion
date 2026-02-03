using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarManager : MonoBehaviour
{
    public int selectedSlot = 0;
    
    // On remplace GameObject[] par ton script GraineSlot
    public Graine[] slots; 

    void Start()
    {
        UpdateUI();
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 scrollVector = context.ReadValue<Vector2>();

            if (scrollVector.y > 0)
            {
                ChangeSlot(-1);
            }
            else if (scrollVector.y < 0)
            {
                ChangeSlot(1);
            }
        }
    }

    private void ChangeSlot(int direction)
    {
        selectedSlot += direction;

        if (selectedSlot > 8) selectedSlot = 0;
        if (selectedSlot < 0) selectedSlot = 8;

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].highlight != null)
            {
                // On active le highlight de l'objet GraineSlot si c'est le bon index
                slots[i].highlight.SetActive(i == selectedSlot);
            }
        }
    }

    // Petite fonction bonus pour récupérer la graine active ailleurs (ex: plantation)
    public Graine GetSelectedSlot()
    {
        return slots[selectedSlot];
    }
}