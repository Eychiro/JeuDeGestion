using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AffichageEcran : MonoBehaviour
{
    public TextMeshProUGUI goldAmount;
    public TextMeshProUGUI timerTimeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreFinalText;
    public TextMeshProUGUI grainesMagiquesobtenues;
    public Button menuPrincipalButton;
    public TextMeshProUGUI GrainesMagiquesMenuPrincipalTxt;

    public static AffichageEcran instance;
    public static int grainesMagiquesTotalesInstance;

    private  ArgentManager argentManager;
    
    private RunPartieManager runPartieManager;
    private int totalScore = 0;
    private int totalGrainesMagiques = 0;

    void Awake()
    {
        instance = this;

        argentManager = GetComponent<ArgentManager>();
        goldAmount.text = argentManager.playerMoney.ToString();
        runPartieManager = GetComponent<RunPartieManager>();
    }

    public void UpdateMoney()
    {
        goldAmount.text = argentManager.playerMoney.ToString();
    }

    public void UpdateScore(int amountGiven)
    {
        totalScore += amountGiven;
        scoreText.text = "Score : " + totalScore.ToString();
    }

    public void CalculateGrainesMagiques()
    {
        totalGrainesMagiques = Mathf.RoundToInt(totalScore / 100f);
    }

    public void GetTotalGrainesMagiques()
    {
        grainesMagiquesTotalesInstance += totalGrainesMagiques;
    }

    private IEnumerator AnimerTxtGrainesMagiques(int valeurCible)
    {
        float tempsEcoule = 0;
        int valeurDepart = 0;

        yield return new WaitForSeconds(1f);

        while (tempsEcoule < 1f)
        {
            tempsEcoule += Time.deltaTime;
            float progression = tempsEcoule / 1f;
            int valeurActuelle = Mathf.RoundToInt(Mathf.Lerp(valeurDepart, valeurCible, progression));
            
            grainesMagiquesobtenues.text = valeurActuelle.ToString();
            
            yield return null;
        }

        grainesMagiquesobtenues.text = valeurCible.ToString();
        menuPrincipalButton.interactable = true;
    }

    public void DisplayFinalScoreAndGrainesMagiques()
    {
        scoreFinalText.text = totalScore.ToString();

        CalculateGrainesMagiques();

        if (totalGrainesMagiques == 0)
        {
            menuPrincipalButton.interactable = true;
            return;
        }
            
        StartCoroutine(AnimerTxtGrainesMagiques(totalGrainesMagiques));
    }

    public void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(runPartieManager.remainingGameTime / 60);
        int seconds = Mathf.FloorToInt(runPartieManager.remainingGameTime % 60);

        timerTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        UpdateTimer();

        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            UpdateScore(50);
        }
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            UpdateScore(10);
        }
    }
}
