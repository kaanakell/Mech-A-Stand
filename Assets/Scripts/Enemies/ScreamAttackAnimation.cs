using UnityEngine;

public class ScreamAttackAnimation : MonoBehaviour
{
    [SerializeField] ScreamAttack screamAttack;

    private void Scream()
    {
        StartCoroutine(screamAttack.CreateSonicWave());
    }
}
