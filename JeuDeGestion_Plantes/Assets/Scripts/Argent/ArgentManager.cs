using UnityEngine;

public class ArgentManager : MonoBehaviour
{
    public static ArgentManager Instance;
    public int playerMoney = 50;

    private AffichageEcran affichageEcran;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        affichageEcran = GetComponent<AffichageEcran>();
    }

    public int GiveMoney(int amountGiven)
    {
        playerMoney += amountGiven;
        affichageEcran.UpdateMoney();
        affichageEcran.UpdateScore(amountGiven);

        return playerMoney;
    }

    public int TakeMoney(int amountTaken)
    {
        playerMoney -= amountTaken;
        affichageEcran.UpdateMoney();

        return playerMoney;
    }
}
