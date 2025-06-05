using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummonManager : MonoBehaviour
{
    [Header("Prefab Settings")]
    [SerializeField] GameObject normalMolePrefab;
    [SerializeField] GameObject bombPrefab;

    [Header("Parent / Position Settings")]
    [SerializeField] Transform moles;
    [SerializeField] Transform[] holes;
    [SerializeField] float yOffset = 0.7f;

    [Header("Spawn Settings")]
    [SerializeField] float spawnInterval = 2f;        // 한 주기마다 등장 시작
    [SerializeField] float spawnDelay = 0.2f;         // 구멍마다 간격
    [SerializeField, Range(0f, 1f)] float bombChance = 0.2f;
    [SerializeField, Range(1, 9)] int maxSpawnPerCycle = 3;

    private List<GameObject> normalMolePool = new List<GameObject>();
    private List<GameObject> bombPool = new List<GameObject>();
    private float timer = 0f;
    private bool isSpawning = false;

    void Start()
    {
        foreach (Transform hole in holes)
        {
            Vector3 spawnPos = hole.position + new Vector3(0f, yOffset, 0f);

            GameObject mole = Instantiate(normalMolePrefab, spawnPos, Quaternion.identity, moles);
            mole.SetActive(false);
            normalMolePool.Add(mole);

            GameObject bomb = Instantiate(bombPrefab, spawnPos, Quaternion.identity, moles);
            bomb.SetActive(false);
            bombPool.Add(bomb);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && !isSpawning)
        {
            StartCoroutine(SpawnSequentially());
            timer = 0f;
        }
       
    }

    IEnumerator SpawnSequentially()
    {
        isSpawning = true;

        List<int> availableIndices = new List<int>();

        for (int i = 0; i < holes.Length; i++)
        {
            if (!normalMolePool[i].activeInHierarchy && !bombPool[i].activeInHierarchy)
                availableIndices.Add(i);
        }

        int spawnCount = Mathf.Min(maxSpawnPerCycle, availableIndices.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            int randIdx = Random.Range(0, availableIndices.Count);
            int selectedIndex = availableIndices[randIdx];
            availableIndices.RemoveAt(randIdx);

            float rand = Random.value;

            if (rand < bombChance)
            {
                GameObject bomb = bombPool[selectedIndex];
                bomb.SetActive(true);
                StartCoroutine(HideAfterDelay(bomb, 1.2f));
            }
            else
            {
                GameObject mole = normalMolePool[selectedIndex];
                mole.SetActive(true);
                StartCoroutine(HideAfterDelay(mole, 1.2f));
            }

            yield return new WaitForSeconds(spawnDelay); // 다음 등장까지 대기
        }

        isSpawning = false;
    }

    IEnumerator HideAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}