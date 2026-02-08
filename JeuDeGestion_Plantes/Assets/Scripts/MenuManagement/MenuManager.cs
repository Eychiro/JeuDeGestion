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

    [Header("SkillTreeMenu")]
    public GameObject skillTreeMenu;
    
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;
    private AudioSource[] audioSources;
    
    public void ShowMenu()
    {
        player.SetActive(false);
        cameraPlayer.SetActive(false);
    }

    private IEnumerator LaunchGameFade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        yield return new WaitForSeconds(0.5f);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(c.r, c.g, c.b, endAlpha);
    }

    private IEnumerator StartGameFade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        yield return new WaitForSeconds(0.5f);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(c.r, c.g, c.b, endAlpha);
        
        cameraMainMenu.SetActive(false);
        MainMenu.SetActive(false);
        grainesMagiques.SetActive(false);

        player.SetActive(true);
        cameraPlayer.SetActive(true);
        fadeImage.gameObject.SetActive(false);
        donneesPlayer.SetActive(true);

        yield return new WaitForSeconds(0.5f);
    }

    public void PlayButton()
    {
        audioSources[1].Play();

        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(StartGameFade(0, 1));
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
        
        PlayerMovement.canMove = true;

        AffichageEcran.instance.GetTotalGrainesMagiques();

        SceneManager.LoadScene(sceneActuelle);
    }

    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        if (!skipMainMenu)
        {
            StartCoroutine(LaunchGameFade(1, 0));
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


    }
}
