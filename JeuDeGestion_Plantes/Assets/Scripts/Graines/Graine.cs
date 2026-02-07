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

    public List<SkillData> skillBoost;

    private Color baseColor;
    private float discountMultiplierBonus = 1f;

    void Start()
    {
        baseColor = texteQuantite.color;
        UpdateSlotUI();

        foreach(SkillData skill in skillBoost)
        {
            if (skill.estDebloquee)
            {
                discountMultiplierBonus -= skill.valeurBonus - 1;
            }
        }

        prixAchat = Mathf.RoundToInt(prixAchat * discountMultiplierBonus);
    }

    public float CalculerTempsMaturation()
    {
        return (prixAchat * 0.5f);
    }

    public int CalculerGainFinMaturation()
    {
        return Mathf.RoundToInt(prixAchat * 1.5f);
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