using UnityEngine;

public class BoutiqueDeGraines : MonoBehaviour, IInteractible
{
    public GameObject shop;
    public InteractionCollider interactionCollider;

    [HideInInspector] public bool joueurProche = false;

    public static bool IsShopOpen = false;
    
    public void OpenShop()
    {
        IsShopOpen = true;
        shop.SetActive(true);
        interactionCollider.parler.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LeaveShop()
    {
        IsShopOpen = false;
        shop.SetActive(false);
        interactionCollider.parler.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Interact() 
    { 
        OpenShop();
    }
}