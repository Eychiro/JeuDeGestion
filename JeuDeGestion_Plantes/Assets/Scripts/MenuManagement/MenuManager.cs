using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    public GameObject cameraPlayer;
    public GameObject donneesPlayer;

    [Header("MainMenu")]
    public GameObject MainMenu;
    public GameObject cameraMainMenu;
    public bool skipMainMenu = false;
    public TextMeshProUGUI grainesMagiquesMenuPrincipalTxt;
    public GameObject grainesMagiques;
    public TextMeshProUGUI madeByTxt;

    [Header("SkillTreeMenu")]
    public GameObject skillTreeMenu;

    [Header("MenuPause")]
    public GameObject menuPause;
    
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;
    private AudioSource[] audioSources;
    
    public void ShowMenu()
    {
        player.SetActive(false);
        cameraPlayer.SetActive(false);
    }

    private void LaunchGameFade(float startAlpha, float endAlpha)
    {
        foreach (Transform bouton in MainMenu.transform)
            bouton.localScale = Vector3.zero;

        Color c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, startAlpha);
        madeByTxt.alpha = 0;
        madeByTxt.rectTransform.anchoredPosition = Vector2.zero;

        Sequence introSequence = DOTween.Sequence();

        introSequence.AppendInterval(0.5f).Append(madeByTxt.DOFade(1f, 2f)).Join(madeByTxt.rectTransform.DOShakePosition(2f, strength: 10f, vibrato: 15).SetRelative(true))
            .Append(madeByTxt.DOFade(0f, 0.8f)).AppendInterval(0.3f);

        float momentDebutFadeNoir = introSequence.Duration();
        introSequence.Append(fadeImage.DOFade(endAlpha, fadeDuration).SetEase(Ease.Linear));
        
        float timingApparition = momentDebutFadeNoir + (fadeDuration * 0.5f); 
        float boutonDelay = 0f;
        foreach (Transform bouton in MainMenu.transform)
        {
            introSequence.Insert(timingApparition + boutonDelay, bouton.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
            boutonDelay += 0.1f;
        }

        introSequence.OnComplete(() => {
            fadeImage.gameObject.SetActive(false);
            madeByTxt.gameObject.SetActive(false);
        });
    }

    private void StartGameFade(float startAlpha, float endAlpha)
    {
        Color c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, startAlpha);
        fadeImage.gameObject.SetActive(true);

        Sequence fadeSequence = DOTween.Sequence();

        float boutonDelay = 0f;
        
        foreach (Transform bouton in MainMenu.transform)
        {
            fadeSequence.Insert(boutonDelay, bouton.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack));
            boutonDelay += 0.05f;
        }

        fadeSequence.Insert(0.2f, fadeImage.DOFade(endAlpha, fadeDuration).SetEase(Ease.Linear));

        fadeSequence.OnComplete(() => {
            MainMenu.SetActive(false);
            grainesMagiques.SetActive(false);
            cameraMainMenu.SetActive(false);
            
            player.SetActive(true);
            cameraPlayer.SetActive(true);
            
            DOVirtual.DelayedCall(0.5f, () => { 
                fadeImage.gameObject.SetActive(false);
                donneesPlayer.SetActive(true);
            });
        });
    }

    public void PlayButton()
    {
        audioSources[1].Play();

        Cursor.lockState = CursorLockMode.Locked;

        StartGameFade(0, 1);
    }

    public void SkillTreeButton()
    {
        audioSources[1].Play();

        skillTreeMenu.transform.DOKill();
        skillTreeMenu.transform.localScale = Vector3.zero;

        MainMenu.SetActive(false);
        skillTreeMenu.SetActive(true);
        skillTreeMenu.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

    public void RetourSkillTreeMenuButton()
    {
        audioSources[1].Play();

        skillTreeMenu.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
            MainMenu.SetActive(true);
            skillTreeMenu.SetActive(false);
        });
    }

    public void QuitterButton()
    {
        audioSources[1].Play();

        Application.Quit();
    }

    public void RetourMainMenu()
    {
        string sceneActuelle = SceneManager.GetActiveScene().name;

        audioSources[1].Play();        
        PlayerMovement.canMove = true;

        AffichageEcran.instance.GetTotalGrainesMagiques();
        AffichageEcran.SauvegarderGraines();

        SceneManager.LoadScene(sceneActuelle);
    }

    public void SetMenuPause()
    {
        if (menuPause.activeSelf)
        {
            PauseReprendreButton();
        }
        else
        {
            menuPause.SetActive(true);
            PlayerMovement.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            foreach (Transform enfant in menuPause.transform)
            {
                if (enfant.GetComponent<Button>() != null)
                    enfant.localScale = Vector3.zero;
            }

            float delay = 0f;
            foreach (Transform enfant in menuPause.transform)
            {
                if (enfant.GetComponent<Button>() != null)
                {
                    enfant.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(delay).SetUpdate(true);
                    delay += 0.1f;
                }
            }
        }
    }

    public void PauseReprendreButton()
    {
        float delay = 0f;
        float dureeAnimation = 0.3f;
        int nombreDeBoutons = 0;

        audioSources[1].Play();
        Cursor.lockState = CursorLockMode.Locked;

        foreach (Transform enfant in menuPause.transform)
        {
            if (enfant.GetComponent<Button>() != null)
            {
                nombreDeBoutons++;
                enfant.DOScale(Vector3.zero, dureeAnimation).SetEase(Ease.InBack).SetDelay(delay).SetUpdate(true);
                delay += 0.05f;
            }
        }

        float tempsTotal = (nombreDeBoutons * 0.05f) + dureeAnimation;

        DOVirtual.DelayedCall(tempsTotal, () => {
            menuPause.SetActive(false);
            PlayerMovement.canMove = true;
            Time.timeScale = 1;
        }).SetUpdate(true);
    }

    public void PauseMenuPrincipalButton()
    {
        string sceneActuelle = SceneManager.GetActiveScene().name;
        
        audioSources[1].Play();
        PlayerMovement.canMove = true;
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneActuelle);

        // MainMenu.SetActive(true);

        // Sequence introSequence = DOTween.Sequence();
        // float boutonDelay = 0f;

        // foreach (Transform bouton in MainMenu.transform)
        // {
        //     introSequence.Insert(0 + boutonDelay, bouton.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        //     boutonDelay += 0.1f;
        // }
    }

    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        if (!skipMainMenu)
        {
            LaunchGameFade(1, 0);
            ShowMenu();

            AffichageEcran.ChargerGraines();
            grainesMagiquesMenuPrincipalTxt.text = AffichageEcran.grainesMagiquesTotalesInstance.ToString();
        }
        else
        {
            fadeImage.gameObject.SetActive(false);
            cameraMainMenu.SetActive(false);
            MainMenu.SetActive(false);
            grainesMagiques.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            donneesPlayer.SetActive(true);
        }
    }

    void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            AffichageEcran.grainesMagiquesTotalesInstance += 10;
            grainesMagiquesMenuPrincipalTxt.text = AffichageEcran.grainesMagiquesTotalesInstance.ToString();
            AffichageEcran.SauvegarderGraines();
        }
        
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            AffichageEcran.grainesMagiquesTotalesInstance = Mathf.Clamp(AffichageEcran.grainesMagiquesTotalesInstance - 10, 0, 1000);
            grainesMagiquesMenuPrincipalTxt.text = AffichageEcran.grainesMagiquesTotalesInstance.ToString();
            AffichageEcran.SauvegarderGraines();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
            SetMenuPause();
    }
}
