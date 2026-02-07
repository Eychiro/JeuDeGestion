using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour
{
    public static InfoPanelManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI nomTxt;
    public TextMeshProUGUI descTxt;
    public TextMeshProUGUI prixTxt;
    public Button boutonAchat;

    private SkillData skillActuel;

    void Awake() => instance = this;

    public void AfficherDetails(SkillData data)
    {
        skillActuel = data;
        
        nomTxt.text = data.nom;
        descTxt.text = data.description; // Ajoute cette variable dans ton ScriptableObject !
        prixTxt.text = "Coût : " + data.prixGraines;

        // On affiche le panneau s'il était caché
        gameObject.SetActive(true);

        // On met à jour l'état du bouton d'achat (si déjà acheté ou trop cher)
        ActualiserBoutonAchat();
    }

    public void ActualiserBoutonAchat()
    {
        if (skillActuel.estDebloquee)
        {
            boutonAchat.interactable = false;
            prixTxt.text = "Débloqué !";
        }
        else
        {
            boutonAchat.interactable = (AffichageEcran.grainesMagiquesTotalesInstance >= skillActuel.prixGraines);
        }
    }

    public void ClicAcheter()
    {
        if (AffichageEcran.grainesMagiquesTotalesInstance >= skillActuel.prixGraines)
        {
            AffichageEcran.grainesMagiquesTotalesInstance -= skillActuel.prixGraines;
            skillActuel.estDebloquee = true;
            
            // On rafraîchit l'UI
            ActualiserBoutonAchat();
            //AffichageEcran.instance.DisplayTotalGrainesMagiques(); 
        }
    }
}