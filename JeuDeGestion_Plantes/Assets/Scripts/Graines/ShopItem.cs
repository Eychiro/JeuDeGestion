using UnityEngine;
using TMPro;
using System.Collections;

public class ShopItem : MonoBehaviour
{

    public int slotIndex;
    public TextMeshProUGUI prixTexte;

    private Color baseColor;

    void Start()
    {
        baseColor = prixTexte.color;

        if (GraineManager.Instance != null)
        {
            int prix = GraineManager.Instance.hotbar.slots[slotIndex].prixAchat;
            prixTexte.text = prix + " Gold";
        }
    }

    public IEnumerator FlashPrixRouge()
    {
        for (int i = 0; i < 3; i++)
        {
            prixTexte.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            prixTexte.color = baseColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Acheter()
    {
        bool success = GraineManager.Instance.AcheterGraineParIndex(slotIndex);

        if (!success)
        {
            StopAllCoroutines();
            StartCoroutine(FlashPrixRouge());
        }
    }
}