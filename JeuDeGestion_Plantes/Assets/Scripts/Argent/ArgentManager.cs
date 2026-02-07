using System.Collections.Generic;
using UnityEngine;

public class ArgentManager : MonoBehaviour
{
    public static ArgentManager Instance;
    public int playerMoney = 50;
    public List<SkillData> skillBoost;

    private AffichageEcran affichageEcran;
    private float multiplicateurBonus = 1f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        affichageEcran = GetComponent<AffichageEcran>();
    }

    void Start()
    {
        foreach (SkillData skill in skillBoost)
        {
            if (skill.estDebloquee)
            {
                multiplicateurBonus += skill.valeurBonus - 1f;
            }
        }
    }

    public int GiveMoney(int amountGiven)
    {
        int montantFinal = Mathf.RoundToInt(amountGiven * multiplicateurBonus);

        playerMoney += montantFinal;

        affichageEcran.UpdateMoney();
        affichageEcran.UpdateScore(montantFinal);

        return playerMoney;
    }

    public int TakeMoney(int amountTaken)
    {
        playerMoney -= amountTaken;
        affichageEcran.UpdateMoney();

        return playerMoney;
    }
}
