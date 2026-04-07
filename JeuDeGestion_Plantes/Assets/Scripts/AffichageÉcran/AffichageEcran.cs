using System.Collections;
using DG.Tweening;
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
    public TextMeshProUGUI bestScoreTxt;
    public TextMeshProUGUI grainesMagiquesobtenues;
    public Button menuPrincipalButton;
    public TextMeshProUGUI GrainesMagiquesMenuPrincipalTxt;

    public static AffichageEcran instance;
    public static int grainesMagiquesTotalesInstance;

    public GameObject setNewBestScore;

    public GameObject moneyPopupPrefab;

    [Header("UI Setup")]
    public Transform scorePopupContainer;
    public Transform moneyPopupContainer;

    public Color warningColor = Color.red;
    public float flashDuration = 0.2f;
    public int numberOfFlashes = 3;

    private Color originalColor;

    private  ArgentManager argentManager;
    private RunPartieManager runPartieManager;

    private int totalScore = 0;
    private static int bestScore = 0;
    private int totalGrainesMagiques = 0;

    private int lastMoney;

    void Awake()
    {
        instance = this;

        argentManager = GetComponent<ArgentManager>();
        runPartieManager = GetComponent<RunPartieManager>();

        ChargerBestScore();
    }

    void Start()
    {
        goldAmount.text = argentManager.playerMoney.ToString();

        lastMoney = argentManager.playerMoney;

        originalColor = timerTimeText.color;
    }

    public void UpdateMoney()
    {
        int currentMoney = argentManager.playerMoney;
        int gain = currentMoney - lastMoney;
        
        goldAmount.text = currentMoney.ToString();

        if (gain != 0)
        {
            GameObject popup = Instantiate(moneyPopupPrefab, moneyPopupContainer);
            TextMeshProUGUI popupText = popup.GetComponent<TextMeshProUGUI>();
            CanvasGroup cg = popup.GetComponent<CanvasGroup>();
            RectTransform rect = popup.GetComponent<RectTransform>();

            if (gain > 0)
            {
                popupText.text = "+" + gain;
                popupText.color = Color.yellow;
            }
            else
            {
                popupText.text = gain.ToString();
                popupText.color = Color.red;
            }

            cg.alpha = 0;
            popup.transform.localPosition = new Vector3(-30, 0, 0);

            Sequence s = DOTween.Sequence();
            
            s.Append(popup.transform.DOLocalMoveX(0, 0.4f).SetEase(Ease.OutBack));
            s.Join(cg.DOFade(1, 0.4f));

            s.AppendInterval(1f);

            s.Append(popup.transform.DOLocalMoveX(100, 0.5f).SetEase(Ease.InQuad));
            s.Join(cg.DOFade(0, 0.5f));

            s.OnComplete(() => Destroy(popup));
        }

        lastMoney = currentMoney;
    }

    public void UpdateScore(int amountGiven)
    {
        totalScore += amountGiven;
        scoreText.text = "Score : " + totalScore.ToString();
        scoreText.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f);

        GameObject popup = Instantiate(moneyPopupPrefab, scorePopupContainer);
        TextMeshProUGUI popupText = popup.GetComponent<TextMeshProUGUI>();
        CanvasGroup cg = popup.GetComponent<CanvasGroup>();

        popupText.text = "+" + amountGiven;
        popupText.color = Color.green;
        cg.alpha = 0;

        popup.transform.localPosition = new Vector3(-50, 0, 0);

        Sequence s = DOTween.Sequence();

        s.Append(popup.transform.DOLocalMoveX(0, 0.4f).SetEase(Ease.OutBack));
        s.Join(cg.DOFade(1, 0.4f));

        s.AppendInterval(0.8f);

        s.Append(popup.transform.DOLocalMoveX(100, 0.5f).SetEase(Ease.InQuad));
        s.Join(cg.DOFade(0, 0.5f));

        s.OnComplete(() => Destroy(popup));
    }

    public void CalculateGrainesMagiques()
    {
        totalGrainesMagiques = Mathf.RoundToInt(totalScore / 100f);
    }

    public void GetTotalGrainesMagiques()
    {
        grainesMagiquesTotalesInstance += totalGrainesMagiques;
    }

    public static void SauvegarderGraines()
    {
        PlayerPrefs.SetInt("GrainesMagiques", grainesMagiquesTotalesInstance);
        PlayerPrefs.Save();
    }

    public static void ChargerGraines()
    {
        if (!PlayerPrefs.HasKey("GrainesMagiques"))
        {
            PlayerPrefs.SetInt("GrainesMagiques", 5);
            PlayerPrefs.Save();
        }

        grainesMagiquesTotalesInstance = PlayerPrefs.GetInt("GrainesMagiques", 0);
    }

    public static void SauvegarderBestScore()
    {
        PlayerPrefs.SetInt("ScoreTotal", bestScore);
        PlayerPrefs.Save();
    }

    public static void ChargerBestScore()
    {
        bestScore = PlayerPrefs.GetInt("ScoreTotal", 0);
    }

    // pour supprimer le score de playtest
    public static void ResetBestScore()
    {
        bestScore = 0;
        PlayerPrefs.SetInt("ScoreTotal", 0);
        PlayerPrefs.Save();

        grainesMagiquesTotalesInstance = 0;
        PlayerPrefs.SetInt("GrainesMagiques", 0);
        PlayerPrefs.Save();
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

    public void AnimerEnfantsBestScore()
    {
        foreach (Transform enfant in setNewBestScore.transform)
        {
            enfant.DOLocalRotate(new Vector3(0, 0, 5f), 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            enfant.DOScale(1.1f, 0.8f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(0f, 0.5f));

            TextMeshProUGUI texteEnfant = enfant.GetComponent<TextMeshProUGUI>();
            
            if (texteEnfant != null)
            {
                LaunchColorLoop(texteEnfant);
            }
        }
    }

    public void AnimerScoreFinal()
    {
        scoreFinalText.rectTransform.DOLocalRotate(new Vector3(0, 0, 5f), 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void AnimerBestScore()
    {
        bestScoreTxt.transform.DOScale(1.1f, 0.8f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(0f, 0.5f));
        LaunchColorLoop(bestScoreTxt);
    }

    private void LaunchColorLoop(TextMeshProUGUI texte)
    {
        if (texte == null)
            return;

        Color couleurCible = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.8f, 1f);

        texte.DOColor(couleurCible, Random.Range(0.5f, 1.5f)).SetEase(Ease.Linear).OnComplete(() => {
            if (texte != null) LaunchColorLoop(texte);
        }); 
    }

    public void DisplayFinalScoreAndGrainesMagiques()
    {
        scoreFinalText.text = totalScore.ToString();

        if (totalScore > bestScore)
        {
            setNewBestScore.SetActive(true);
            AnimerEnfantsBestScore();

            bestScore = totalScore;

            SauvegarderBestScore();
        }

        bestScoreTxt.text = bestScore.ToString();
        AnimerBestScore();
        AnimerScoreFinal();

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

    public void WarningTimer()
    {
        timerTimeText.DOKill();

        Sequence s = DOTween.Sequence();

        for (int i = 0; i < numberOfFlashes; i++)
        {
            s.Append(timerTimeText.DOColor(warningColor, flashDuration));
            s.Join(timerTimeText.transform.DOPunchScale(Vector3.one * 0.2f, flashDuration * numberOfFlashes));
            s.Append(timerTimeText.DOColor(originalColor, flashDuration));
        }
    }

    void Update()
    {
        UpdateTimer();

        if (Keyboard.current.uKey.wasPressedThisFrame) UpdateScore(50);
        if (Keyboard.current.iKey.wasPressedThisFrame) UpdateScore(10);
        if (Keyboard.current.rKey.wasPressedThisFrame) ResetBestScore();
    }

    private void OnDestroy()
    {        
        if (setNewBestScore != null)
        {
            bestScoreTxt.transform.DOKill(true);

            setNewBestScore.transform.DOKill(true);
            foreach (Transform enfant in setNewBestScore.transform)
            {
                enfant.DOKill(true);
            }
        }

        if (scoreFinalText != null)
        {
            scoreFinalText.transform.DOKill(true);
        }
    }
}
