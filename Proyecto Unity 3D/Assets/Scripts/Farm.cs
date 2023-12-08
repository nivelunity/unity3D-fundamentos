using System.Collections;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField]
    [Range(10, 200)]
    int foodPoints = 100;

    private bool isLooting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLooting = true;
            StartCoroutine(LootingCorrutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLooting = false;
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
