using System.Collections;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField]
    [Range(10, 200)]
    int foodPoints = 100;

    [SerializeField]
    ParticleSystem lootSFX;

    private bool isLooting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLooting = true;
            lootSFX.Play();
            StartCoroutine(LootingCorrutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLooting = false;
            lootSFX.Stop();
            StartCoroutine(LootingCorrutine());
        }
    }

    IEnumerator LootingCorrutine()
    {
        while (isLooting)
        {
            yield return new WaitForSeconds(1f);
            foodPoints--;
        }

        yield return null;
    }
}
