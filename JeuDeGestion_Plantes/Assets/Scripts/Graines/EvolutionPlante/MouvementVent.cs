using UnityEngine;

public class MouvementVent : MonoBehaviour
{
    [Header("Réglages du Vent")]
    public float amplitude = 3f;
    public float vitesse = 2f;
    
    private float offsetAleatoire;

    void Start()
    {
        offsetAleatoire = Random.Range(0f, 100f);
    }

    public void CalculateAndSetRotations()
    {
        float angleZ = Mathf.Sin(Time.time * vitesse + offsetAleatoire) * amplitude;
        float angleX = Mathf.Cos(Time.time * vitesse * 0.6f + offsetAleatoire) * (amplitude * 0.5f);

        transform.localRotation = Quaternion.Euler(angleX, 0, angleZ);
    }

    void Update()
    {
        CalculateAndSetRotations();
    }
}