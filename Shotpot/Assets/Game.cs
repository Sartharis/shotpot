using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct FoodItem
{
    public Food foodToSpawn;
    public float weight;
}

[System.Serializable]
public struct FoodWave
{
    public int foodCount;
    public float waitTime;
}


public class Game : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] private float roundTime;
    [HideInInspector] public float currentTime;
    [HideInInspector] public int winner;
    [SerializeField] private FoodCounter[] counters;
    [SerializeField] private float restartTime;
    public bool showIntro;

    [Header("Food Settings")]
    [SerializeField] private FoodItem[] foodsToSpawn;
    [SerializeField] private FoodWave[] foodWaves;
    [SerializeField] private Collider2D spawnZone;
    private int waveCounter = 0;

    IEnumerator SpawnFoodos()
    {
        yield return new WaitForSeconds(foodWaves[waveCounter].waitTime);
        SpawnWave(foodWaves[waveCounter]);
        waveCounter++;
        if(waveCounter < foodWaves.Length)
        {
           yield return SpawnFoodos();
        }
    }

    FoodItem ChooseFood(FoodItem[] probs)
    {
        float total = 0;

        foreach (FoodItem elem in probs)
        {
            total += elem.weight;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i].weight)
            {
                return probs[i];
            }
            else
            {
                randomPoint -= probs[i].weight;
            }
        }
        return probs[probs.Length - 1];
    }

    void SpawnWave(FoodWave wave)
    {
        for(int i = 0; i < wave.foodCount; i++)
        {
            Vector3 min = spawnZone.bounds.min;
            Vector3 max = spawnZone.bounds.max;
            Instantiate(ChooseFood(foodsToSpawn).foodToSpawn, new Vector3(Random.Range(min.x, max.x),Random.Range(min.y, max.y),Random.Range(min.z, max.z)) , new Quaternion());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = roundTime;
        winner = -1;

    }

    // Update is called once per frame
    void Update()
    {
        if (showIntro)
        {
            if(Input.anyKeyDown)
            {
                showIntro = false;
                StartCoroutine(SpawnFoodos());
            }
        }
        else
        { 
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
                int tempWinner = -1;
                int tempWinnerMax = -1;
                foreach (FoodCounter counter in counters)
                {
                    if (counter.getScore() > tempWinnerMax)
                    {
                        tempWinner = counter.player;
                        tempWinnerMax = counter.getScore();
                    }
                    else if (counter.getScore() == tempWinnerMax)
                    {
                        tempWinner = -1;
                    }
                }
                winner = tempWinner;
                restartTime -= Time.deltaTime;
                if (restartTime < 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
         }
    }
}
