using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class InfoPanelManager : MonoBehaviour
{
    public static InfoPanelManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI nomTxt;
    public TextMeshProUGUI descTxt;
    public TextMeshProUGUI prixTxt;
    public Button boutonAchat;
    public GameObject infoSupTxt;
    public TextMeshProUGUI infoSupTxtSupplementaire;
    public List<Button> pousseRapideListButton;
    public List<Button> marchandageListButton;

    public TextMeshProUGUI grainesMagiquesMenuPrincipalTxt;

    private SkillData skillActuel;
    private GameObject checkBoxActuelle;
    private AudioSource audioSource;

    void Awake() => instance = this;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        infoSupTxtSupplementaire.DOColor(Color.black, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void AfficherDetails(SkillData data)
    {
        DOTween.Kill("Skill_Name");
        skillActuel = data;

        nomTxt.text = data.nom;
        nomTxt.maxVisibleCharacters = 0;
        DOTween.To(() => nomTxt.maxVisibleCharacters, x => nomTxt.maxVisibleCharacters = x, data.nom.Length, 1f).SetId("Skill_Name").SetEase(Ease.Linear);

        descTxt.text = data.description;
        prixTxt.text = "Coût : " + data.prixGraines;
        infoSupTxtSupplementaire.text = data.descriptionSupplementaire;
    

        if (infoSupTxtSupplementaire.text == "")
            infoSupTxtSupplementaire.gameObject.SetActive(false);
        else
        {
            infoSupTxtSupplementaire.gameObject.SetActive(true);
        }

        gameObject.SetActive(true);

        ActualiserBoutonAchat();
    }

    public void ShowInfoSupp()
    {
        infoSupTxt.transform.DOKill();

        if (skillActuel.typeDeBonus == TypeBonus.ReductionShopPrice)
        {
            foreach(Button button in pousseRapideListButton)
            {
                ColorBlock cb = button.colors;
                cb.normalColor = Color.yellow; 
                button.colors = cb;
            }
        }
        else if (skillActuel.typeDeBonus == TypeBonus.ReductionDeVitesseDePousse)
        {
            foreach(Button button in marchandageListButton)
            {
                ColorBlock cb = button.colors;
                cb.normalColor = Color.yellow;
                button.colors = cb;
            }
        }

        if (!infoSupTxt.activeSelf)
        {
            infoSupTxt.transform.localScale = Vector3.zero;
            infoSupTxt.SetActive(true);
            infoSupTxt.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
    }

    public void HideInfoSupp()
    {
        infoSupTxt.transform.DOKill();

        if (skillActuel.typeDeBonus == TypeBonus.ReductionShopPrice)
        {
            foreach(Button button in pousseRapideListButton)
            {
                ColorBlock cb = button.colors;
                cb.normalColor = new Color32(115, 115, 115, 255); 
                button.colors = cb;
            }
        }
        else if (skillActuel.typeDeBonus == TypeBonus.ReductionDeVitesseDePousse)
        {
            foreach(Button button in marchandageListButton)
            {
                ColorBlock cb = button.colors;
                cb.normalColor = new Color32(115, 115, 115, 255); 
                button.colors = cb;
            }
        }

        if (infoSupTxt.activeSelf)
            infoSupTxt.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
                infoSupTxt.SetActive(false);
            });
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
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();

            AffichageEcran.grainesMagiquesTotalesInstance -= skillActuel.prixGraines;
            skillActuel.estDebloquee = true;
            
            ActualiserBoutonAchat();
            grainesMagiquesMenuPrincipalTxt.text = AffichageEcran.grainesMagiquesTotalesInstance.ToString();
            AffichageEcran.SauvegarderGraines();
        }
    }

    void OnDestroy()
    {
        infoSupTxtSupplementaire.DOKill();
    }
}