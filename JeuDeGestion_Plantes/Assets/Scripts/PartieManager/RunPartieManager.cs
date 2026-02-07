using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class RunPartieManager : MonoBehaviour
{
    public float remainingGameTime;
    public GameObject MenuEndPartie;
    public BoutiqueDeGraines boutiqueDeGraines;
    public GameObject hotBar;

    private void EndActualGame()
    {
        if (BoutiqueDeGraines.IsShopOpen)
            boutiqueDeGraines.LeaveShop();

        if (boutiqueDeGraines.interactionCollider.isActiveAndEnabled)
            boutiqueDeGraines.interactionCollider.parler.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerMovement.canMove = false;

        hotBar.SetActive(false);        
        MenuEndPartie.SetActive(true);

        AffichageEcran.instance.DisplayFinalScoreAndGrainesMagiques();
    }

    void Update()
    {
        if (remainingGameTime > 0)
        {
            remainingGameTime -= Time.deltaTime;
        }
        else if (remainingGameTime < 0)
        {
            remainingGameTime = 0;
            EndActualGame();
        }
    }
}
