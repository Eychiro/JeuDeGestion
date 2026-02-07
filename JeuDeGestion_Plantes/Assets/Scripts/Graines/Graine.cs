using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Graine : MonoBehaviour
{
    [Header("Paramètres de la Graine")]
    public GameObject prefabPlante;
    public int prixAchat = 10;
    public int quantiteDetenue = 0;

    [Header("Références UI")]
    public TextMeshProUGUI texteQuantite;
    public GameObject highlight;
    public Image iconeGraine;

    public List<SkillData> skillBoostShopPrice;
    public List<SkillData> skillBoostGrowthReductionTime;

    private Color baseColor;
    private int prixAchatDepart = 0;
    private float discountMultiplierBonus = 1f;
    private int ReductionGrowthTime = 0;

    void Start()
    {
        baseColor = texteQuantite.color;
        UpdateSlotUI();

        prixAchatDepart = prixAchat;

        foreach(SkillData skill in skillBoostShopPrice)
        {
            if (skill.estDebloquee)
            {
                discountMultiplierBonus -= skill.valeurBonus - 1;
            }
        }

        prixAchat = Mathf.RoundToInt(prixAchat * discountMultiplierBonus);

        foreach(SkillData skill in skillBoostGrowthReductionTime)
        {
            if (skill.estDebloquee)
            {
                ReductionGrowthTime += (int)skill.valeurBonus;
            }
        } 
    }

    public float CalculerTempsMaturation()
    {
        return (prixAchat * 0.5f) - ReductionGrowthTime;
    }

    public int CalculerGainFinMaturation()
    {
        return Mathf.RoundToInt(prixAchatDepart * 1.5f);
    }

    public void UpdateSlotUI()
    {
        texteQuantite.text = "x" + quantiteDetenue;

        if (quantiteDetenue == 0)
            texteQuantite.color = Color.red;
        else
            texteQuantite.color = baseColor;
    }
}