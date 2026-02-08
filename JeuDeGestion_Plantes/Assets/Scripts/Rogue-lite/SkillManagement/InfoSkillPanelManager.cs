using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;


public class InfoPanelManager : MonoBehaviour
{
    public static InfoPanelManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI nomTxt;
    public TextMeshProUGUI descTxt;
    public TextMeshProUGUI prixTxt;
    public Button boutonAchat;
    public GameObject infoSupButton;
    public GameObject infoSupTxt;

    public TextMeshProUGUI grainesMagiquesMenuPrincipalTxt;

    private SkillData skillActuel;
    private GameObject checkBoxActuelle;

    void Awake() => instance = this;

    public void AfficherDetails(SkillData data)
    {
        skillActuel = data;
        
        nomTxt.text = data.nom;
        descTxt.text = data.description;
        prixTxt.text = "Coût : " + data.prixGraines;

        gameObject.SetActive(true);

        ActualiserBoutonAchat();
    }

    public void ToggleInfoSupp()
    {
        infoSupTxt.transform.DOKill();

        if (!infoSupTxt.activeSelf)
        {
            infoSupTxt.transform.localScale = Vector3.zero;
            infoSupTxt.SetActive(true);

            infoSupTxt.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
        else
        {
            infoSupTxt.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
                infoSupTxt.SetActive(false);
            });
        }
    }
    
    public void SetActiveCheckBox(GameObject checkedBox)
    {
        checkBoxActuelle = checkedBox;
    }

    public void ActualiserBoutonAchat()
    {
        bool aLePrerequis = skillActuel.skillRequis == null || skillActuel.skillRequis.estDebloquee;
        bool aAssezDargent = AffichageEcran.grainesMagiquesTotalesInstance >= skillActuel.prixGraines;

        if (skillActuel.estDebloquee)
        {
            boutonAchat.interactable = false;
            prixTxt.fontSize = 36;
            prixTxt.text = "Débloqué !";
            checkBoxActuelle.SetActive(true);
        }
        else if (!aLePrerequis)
        {
            boutonAchat.interactable = false;
            prixTxt.fontSize = 20;
            prixTxt.text = "(Niveau précédent requis)";
        }
        else
        {
            boutonAchat.interactable = aAssezDargent;
            prixTxt.fontSize = 36;
            prixTxt.text = "Coût : " + skillActuel.prixGraines;
        }
    }

    public void ClicAcheter()
    {
        if (AffichageEcran.grainesMagiquesTotalesInstance >= skillActuel.prixGraines)
        {
            AffichageEcran.grainesMagiquesTotalesInstance -= skillActuel.prixGraines;
            skillActuel.estDebloquee = true;
            
            ActualiserBoutonAchat();
            grainesMagiquesMenuPrincipalTxt.text = AffichageEcran.grainesMagiquesTotalesInstance.ToString(); 
        }
    }
}