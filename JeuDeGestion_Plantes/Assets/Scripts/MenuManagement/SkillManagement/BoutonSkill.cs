using UnityEngine;
using UnityEngine.UI;

public class BoutonSkill : MonoBehaviour
{
    public SkillData donneeDeCeSkill; // On glisse ici le fichier créé à l'étape 2
    public Image iconeBouton;

    [SerializeField] private GameObject overlayFermeture;

    void Start()
    {
        // On affiche l'image définie dans le SkillData sur le bouton
        if (donneeDeCeSkill.icone != null)
            iconeBouton.sprite = donneeDeCeSkill.icone;

        InfoPanelManager.instance.gameObject.SetActive(false);
    }

    public void AuClicDuBouton()
    {
        InfoPanelManager.instance.gameObject.SetActive(true);
        overlayFermeture.SetActive(true); // On affiche le bloqueur de clic
        InfoPanelManager.instance.AfficherDetails(donneeDeCeSkill);
    }
}