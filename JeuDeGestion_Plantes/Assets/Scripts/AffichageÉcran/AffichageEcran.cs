using TMPro;
using UnityEngine;

public class AffichageEcran : MonoBehaviour
{
    public TextMeshProUGUI goldAmount;
    [SerializeField] public TextMeshProUGUI timerTimeText;
    [SerializeField] public TextMeshProUGUI scoreText;

    private  ArgentManager argentManager;
    
    private RunPartieManager runPartieManager;
    private int totalScore = 0;

    void Awake()
    {
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

    public void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(runPartieManager.remainingGameTime / 60);
        int seconds = Mathf.FloorToInt(runPartieManager.remainingGameTime % 60);

        timerTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        UpdateTimer();
    }
}
