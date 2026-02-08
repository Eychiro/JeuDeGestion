using System;
using UnityEngine;

public enum TypeBonus { Argent, ReductionDeVitesseDePousse, TempsPartieSupplementaire, ReductionShopPrice, PlayerSpeed, UnlockableGraine }

[CreateAssetMenu(fileName = "NouveauSkill", menuName = "MonJeu/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("Infos de base")]
    public string nom;
    [TextArea] public string description;
    public int prixGraines;
    
    [Header("Type de compétence")]
    public TypeBonus typeDeBonus;

    [Header("Valeur du bonus")]
    [Range(1.01f, 30)] public float valeurBonus;

    [Header("Prérequis")]
    public SkillData skillRequis;

    [Header("État")]
    public bool estDebloquee;

    [Header("Index de l'Item")]
    [Range(3, 8)] public int itemIndex;
}