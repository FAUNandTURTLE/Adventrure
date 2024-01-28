using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerEye : MonoBehaviour
{

    private Day1nigt daynight;
    [SerializeField] GameObject enemy;
    [SerializeField] float delaySpawn;
    public bool day;

    [Header("шанс спавна")]
    [SerializeField] int ChanceSpawn;
    void Start()
    {
        daynight = FindObjectOfType<Day1nigt>();
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        while (true)
        {
            if (daynight.isDay == day)
            {
                int randomNumber = Random.Range(0, 100);
                if (randomNumber <= ChanceSpawn)
                {
                    Instantiate(enemy, transform.position, enemy.transform.rotation);
                }
           
                yield return new WaitForSeconds(delaySpawn);
            }
            else
                yield return new WaitForSeconds(1);
        }
    }

}
