using System.Collections;
using UnityEngine;

public class Farm : MonoBehaviour
{
 
    [SerializeField]
    ParticleSystem lootSFX;

    [SerializeField]
    Renderer floorRenderer;

    [SerializeField]
    Material lootMaterial;

    [SerializeField]
    [Range(1, 9)]
    int foodPoints = 9;

    [SerializeField]
    [Range(9, 18)]
    int lootTolalTime = 10;

    private bool isLooting = false;
    private Material originalMaterial;
    private WaitForSeconds waitLoot;
    private int foodIndex = 0;

    private void OnEnable()
    {
        originalMaterial = floorRenderer.material;
        waitLoot = new WaitForSeconds(lootTolalTime/foodPoints);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !IsFarmEmpty())
        {
            isLooting = true;
            LootingFarm();
            StartCoroutine(LootingCorrutine());
        }
    }

    private void LootingFarm()
    {
        lootSFX.Play();
        floorRenderer.material = lootMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsFarmEmpty()) return;

        if (other.gameObject.CompareTag("Player"))
        {
            isLooting = false;
            CancelLooting();
            StartCoroutine(LootingCorrutine());
        }
    }

    private void CancelLooting()
    {
        floorRenderer.material = originalMaterial;
        lootSFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    IEnumerator LootingCorrutine()
    {
        while (isLooting)
        {
            yield return waitLoot;

            if (!isLooting) break;
            foodPoints--;
            transform.GetChild(foodIndex).gameObject.SetActive(false);
            foodIndex++;

            if (IsFarmEmpty())
            {
                isLooting = false;
                CancelLooting();
                Debug.Log("GRANJA LOOTEADA");
            }
        }
    }

    private bool IsFarmEmpty()
    {
        return foodPoints == 0;
    }
}
