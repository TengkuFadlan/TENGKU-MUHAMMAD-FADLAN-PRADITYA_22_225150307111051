using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSummon : MonoBehaviour
{

    public int maxSummons = 5;

    public GameObject bugPrefab;
    public GameObject spitterPrefab;
    public GameObject boomerPrefab;
    public GameObject summonSpot;

    float nextHealthSummon;

    BaseHealth baseHealth;
    List<GameObject> summonList = new();

    void OnEnable()
    {
        baseHealth = gameObject.GetComponent<BaseHealth>();
        baseHealth.OnDeath += HandleDeath;
        baseHealth.OnHealthChanged += HandlHealthChanged;
    }

    void Start()
    {
        nextHealthSummon = baseHealth.health - (baseHealth.maxHealth/(maxSummons+1));
    }

    void OnDisable()
    {
        baseHealth.OnDeath -= HandleDeath;
        baseHealth.OnHealthChanged -= HandlHealthChanged;
    }

    void HandlHealthChanged()
    {
        if (baseHealth.health == 0)
            return;
        if (baseHealth.health > nextHealthSummon)
            return;
        
        nextHealthSummon -= baseHealth.maxHealth/(maxSummons+1);

        SummonBug();
    }

    void HandleDeath()
    {
        foreach (GameObject summon in summonList)
        {

            if (!summon)
            {
                continue;
            }

            if (summon.TryGetComponent<BaseHealth>(out BaseHealth summonHealth))
            {
                summonHealth.TakeDamage(summonHealth.health + 100);
            }
        }

        summonList.Clear();

        Destroy(this);
    }

    void SummonBug()
    {
        int rng = Random.Range(1, 4);
        GameObject targetSummon = rng switch
        {
            1 => bugPrefab,
            2 => spitterPrefab,
            3 => boomerPrefab,
            _ => boomerPrefab,
        };
        GameObject summonedBug = Instantiate(targetSummon, summonSpot.transform.position, Quaternion.identity);

        summonList.Add(summonedBug);
    }
}