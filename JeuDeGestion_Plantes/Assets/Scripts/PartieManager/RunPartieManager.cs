using UnityEngine;
using System.Collections.Generic;

public class RunPartieManager : MonoBehaviour
{
    public float remainingGameTime;
    public GameObject MenuEndPartie;
    public BoutiqueDeGraines boutiqueDeGraines;
    public GameObject hotBar;
    public List<SkillData> skillBoost;
    public AudioSource warningTimer;

    private float bonusSkill = 0;
    private bool _animationCritique = false;

    void Start()
    {
        foreach (SkillData skill in skillBoost)
        {
            if (skill.estDebloquee)
            {
                bonusSkill += skill.valeurBonus;
            }
        }

        remainingGameTime += bonusSkill;
    }

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

        if (remainingGameTime < 60 && !_animationCritique)
        {
            _animationCritique = true;
            warningTimer.Play();

            AffichageEcran.instance.WarningTimer();
        }
    }
}
