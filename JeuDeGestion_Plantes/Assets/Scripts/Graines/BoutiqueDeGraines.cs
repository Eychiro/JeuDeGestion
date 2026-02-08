using DG.Tweening;
using UnityEngine;

public class BoutiqueDeGraines : MonoBehaviour, IInteractible
{
    public GameObject shop;
    public InteractionCollider interactionCollider;

    [HideInInspector] public bool joueurProche = false;

    public static bool IsShopOpen = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenShop()
    {
        shop.transform.DOKill();

        IsShopOpen = true;
        shop.transform.localScale = Vector3.zero;
        
        shop.SetActive(true);
        shop.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        interactionCollider.parler.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LeaveShop()
    {
        audioSource.Play();
        IsShopOpen = false;

        shop.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
            shop.SetActive(false);
        });
        interactionCollider.parler.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Interact() 
    { 
        OpenShop();
    }
}