using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BoutonSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillData donneeDeCeSkill;
    public GameObject checkedBox;

    [Header("Skill Name Info")]
    public GameObject skillNameInfo;
    public TextMeshProUGUI skillName;

    [SerializeField] private GameObject overlayFermeture;
    [SerializeField] private Vector2 offset = new Vector2(0, 50f);

    private bool isHovering = false;

    void Start()
    {
        if (donneeDeCeSkill.estDebloquee)
            checkedBox.SetActive(true);

        InfoPanelManager.instance.gameObject.SetActive(false);
        
        if (skillNameInfo != null)
            skillNameInfo.SetActive(false);
    }

    void Update()
    {
        if (isHovering && skillNameInfo != null)
        {
            skillNameInfo.transform.position = Mouse.current.position.ReadValue() + offset;
        }
    }

    public void AuClicDuBouton()
    {
        InfoPanelManager.instance.gameObject.SetActive(true);
        overlayFermeture.SetActive(true);
        InfoPanelManager.instance.SetActiveCheckBox(checkedBox);
        InfoPanelManager.instance.AfficherDetails(donneeDeCeSkill);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        skillName.text = donneeDeCeSkill.nom;

        if (skillNameInfo != null)
            skillNameInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (skillNameInfo != null)
            skillNameInfo.SetActive(false);
    }
}