using UnityEngine;

public class GraineManager : MonoBehaviour
{
    public static GraineManager Instance;
    
    public HotbarManager hotbar {get; private set;}
    
    void Awake()
    {
        hotbar = GetComponent<HotbarManager>();
        Instance = this;
    }

    public void AjouterGraineSelectionnee(int index, int montant)
    {
        Graine slotCible = hotbar.slots[index];
        if (slotCible != null)
        {
            slotCible.quantiteDetenue += montant;
            slotCible.UpdateSlotUI();
        }
    }
    
    public bool RetirerGraineSelectionnee(int montant)
    {
        Graine slotActif = hotbar.GetSelectedSlot();
        
        if (slotActif != null && slotActif.quantiteDetenue >= montant)
        {
            slotActif.quantiteDetenue -= montant;
            slotActif.UpdateSlotUI();
            return true;
        }

        Debug.LogWarning("Pas assez de graines !");
        return false;
    }

    public bool AcheterGraineParIndex(int index)
    {
        Graine slotCible = hotbar.slots[index];
        int prix = slotCible.prixAchat;

        if (ArgentManager.Instance.playerMoney >= prix)
        {
            ArgentManager.Instance.TakeMoney(prix);
            AjouterGraineSelectionnee(index, 1);
            return true;
        }
        else
        {
            Debug.Log("Pas assez d'argent !");
            return false;
        }
    }
}