using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class RunPartieManager : MonoBehaviour
{
    public float remainingGameTime;

    private void EndActualGame()
    {
        // Afficher menu (un seul bouton revenir au menu principal) avec score total de la partie au milieu de l'écran
    }

    void Update()
    {
        if (remainingGameTime > 0)
        {
            remainingGameTime -= Time.deltaTime;
        }
        else if (remainingGameTime < 0)
        {
            remainingGameTime = 0;
            EndActualGame();
        }
    }
}
