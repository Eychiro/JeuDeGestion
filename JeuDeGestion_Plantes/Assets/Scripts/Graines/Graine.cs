using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Graine : MonoBehaviour
{
    [Header("Paramètres de la Graine")]
    public GameObject prefabPlante;
    public int prixAchat = 10;
    public float tempsDePousse = 5.0f;
    public int quantiteDetenue = 0;

    [Header("Références UI")]
    public TextMeshProUGUI texteQuantite;
    public GameObject highlight;
    public Image iconeGraine;

    private Color baseColor;

    void Start()
    {
        baseColor = texteQuantite.color;
        UpdateSlotUI();
    }

    public float CalculerTempsMaturation()
    {
        return prixAchat * 0.5f;
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