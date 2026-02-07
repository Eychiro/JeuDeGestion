using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

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
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(StartGameFade(0, 1));
    }

    public void SkillTreeButton()
    {
        MainMenu.SetActive(false);
        skillTreeMenu.SetActive(true);
    }

    public void RetourSkillTreeMenuButton()
    {
        skillTreeMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void QuitterButton()
    {
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
        if (!skipMainMenu)
        {
            StartCoroutine(LaunchGameFade(1, 0));
            ShowMenu();

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
        }

    }
}
