using TMPro;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{
    public GameObject parler;
    public string textInteraction;

    [HideInInspector] public bool joueurProche = false;

    void Start()
    {
        parler.SetActive(true);

        parler.GetComponentInChildren<TextMeshProUGUI>().ForceMeshUpdate();

        parler.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parler.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textInteraction;
            parler.SetActive(true);
            joueurProche = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parler.SetActive(false);
            joueurProche = false;
        }
    }

}
