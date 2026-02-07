using UnityEngine;

[CreateAssetMenu(fileName = "NouveauSkill", menuName = "MonJeu/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("Infos de base")]
    public string nom;
    [TextArea] public string description;
    public int prixGraines;
    public Sprite icone; // Pour afficher une image dans ton menu

    [Header("État")]
    public bool estDebloquee;

    [Header("Effets du Skill")]
    public float multiplicateurArgent = 1f; // Exemple : 1.2f pour +20%
    public float reductionTempsDePousse = 0f;      // Exemple : -5s sur la pousse
}