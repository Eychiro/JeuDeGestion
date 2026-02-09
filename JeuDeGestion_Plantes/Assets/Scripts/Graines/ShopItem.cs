using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    public int slotIndex;
    public TextMeshProUGUI prixTexte;
    public SkillData unlockableItem;

    private Image imageIcon;

    private AudioSource[] audioSource;
    private Color baseColor;

    void Awake()
    {
        imageIcon = transform.GetChild(1).GetComponent<Image>();
    }

    void Start()
    {
        if (unlockableItem != null && unlockableItem.estDebloquee)
            gameObject.SetActive(true);
        else if (unlockableItem != null && !unlockableItem.estDebloquee)
            gameObject.SetActive(false);

        audioSource = GetComponents<AudioSource>();
        baseColor = prixTexte.color;

        if (GraineManager.Instance != null)
        {
            int prix = GraineManager.Instance.hotbar.slots[slotIndex].prixAchat;
            prixTexte.text = prix + " Gold";
        }

        float dureeAleatoire = Random.Range(1.5f, 2.5f);
        float delaiAleatoire = Random.Range(0f, 1f);

        imageIcon.rectTransform.DOLocalMoveY(10f, dureeAleatoire).SetRelative(true).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetDelay(delaiAleatoire);
    }

    public void SignalReussitePrix()
    {
        prixTexte.transform.DOKill(true); 
        prixTexte.DOKill(true);

        prixTexte.DOColor(Color.green, 0.2f).OnComplete(() => {
            prixTexte.DOColor(baseColor, 0.5f);
        });
        
        prixTexte.transform.DOShakePosition(0.5f, 10f);
    }

    public void SignalErreurPrix()
    {
        prixTexte.transform.DOKill(true); 
        prixTexte.DOKill(true);

        prixTexte.DOColor(Color.red, 0.2f).OnComplete(() => {
            prixTexte.DOColor(baseColor, 0.5f);
        });
        
        prixTexte.transform.DOShakePosition(0.5f, 10f);
    }

    public void Acheter()
    {
        bool success = GraineManager.Instance.AcheterGraineParIndex(slotIndex);

        if (success)
        {
            audioSource[0].pitch = Random.Range(0.9f, 1.1f);
            audioSource[0].Play();

            SignalReussitePrix();
        }
        else
        {
            audioSource[1].pitch = Random.Range(0.9f, 1.1f);
            audioSource[1].Play();

            SignalErreurPrix();
        }
    }

    void OnDestroy()
    {
        imageIcon.rectTransform.DOKill(true);
    }
}