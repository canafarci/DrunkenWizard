using UnityEngine;

[CreateAssetMenu(fileName = "FXObjects", menuName = "JammyGame/FXObjects", order = 0)]
public class FXObjects : ScriptableObject
{
    public GameObject PlayerDMG, EnemyDMG, EnemyDeath;

    public AudioClip BG, SpellCastSFX, PlayerDMGSFX, EnemyDMGSFX;

}