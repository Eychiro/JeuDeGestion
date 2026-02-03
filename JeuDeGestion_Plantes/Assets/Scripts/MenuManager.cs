using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

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
        
    }

    public void QuitterButton()
    {
        Application.Quit();
    }

    void Start()
    {
        if (!skipMainMenu)
        {
            StartCoroutine(LaunchGameFade(1, 0));
            ShowMenu();
        }
        else
        {
            fadeImage.gameObject.SetActive(false);
            cameraMainMenu.SetActive(false);
            MainMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            donneesPlayer.SetActive(true);
        }
    }
}
