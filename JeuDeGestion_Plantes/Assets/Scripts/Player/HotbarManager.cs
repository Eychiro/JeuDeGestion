using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarManager : MonoBehaviour
{
    public int selectedSlot = 0;
    public Graine[] slots;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        CheckAlphaNumericKeys();
    }

    private void CheckAlphaNumericKeys()
    {
        for (int i = 0; i < 9; i++)
        {
            Key key = (Key)((int)Key.Digit1 + i);
            
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                if (selectedSlot != i)
                {
                    selectedSlot = i;
                    audioSource.Play();
                    UpdateUI();
                }
                break;
            }
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 scrollVector = context.ReadValue<Vector2>();
            
            if (scrollVector.y > 0)
            {
                audioSource.Play();
                ChangeSlot(-1);
            }
            else if (scrollVector.y < 0)
            {
                audioSource.Play();
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
                slots[i].highlight.SetActive(i == selectedSlot);
            }
        }
    }

    public Graine GetSelectedSlot()
    {
        return slots[selectedSlot];
    }
}