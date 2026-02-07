using UnityEngine;
using UnityEngine.UI;

public class BoutonSkill : MonoBehaviour
{
    public SkillData donneeDeCeSkill;
    public GameObject checkedBox;

    [SerializeField] private GameObject overlayFermeture;

    void Start()
    {
        if (donneeDeCeSkill.estDebloquee)
            checkedBox.SetActive(true);

        InfoPanelManager.instance.gameObject.SetActive(false);
    }

    public void AuClicDuBouton()
    {
        InfoPanelManager.instance.gameObject.SetActive(true);
        overlayFermeture.SetActive(true);
        InfoPanelManager.instance.SetActiveCheckBox(checkedBox);
        InfoPanelManager.instance.AfficherDetails(donneeDeCeSkill);
        
    }
}