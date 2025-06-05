using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMng : MonoBehaviour
{
    [SerializeField] GameObject EnemyA;
    [SerializeField] GameObject EnemyB;
   public GameObject Boss;
    [SerializeField] Transform StoreEnemy;
    [SerializeField] Text BossSpawnText;
     public Image BossHpFill_Img;
      public GameObject BossHpBar;
    float SpawnTime = 2.0f;
        float TimeCheck = 0;
    Vector3 RandomPosA;
    Vector3 RandomPosB;
    Vector3 Pos = new Vector3 (15, 0, 0);
    bool IsBoss = false;
    bool IsSpawn = false;
    float BossSpawnTime = 0;
    public float CheckSpawnTime = 60;
    public int CheckSpawnTimeCheck = 60; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckSpawnTime -= Time.deltaTime;
        CheckSpawnTimeCheck = (int)CheckSpawnTime;
        if (IsBoss == false)
        {
        BossSpawnText.text = "보스 출현까지 " + CheckSpawnTimeCheck.ToString() + "초";  //보스 스폰시간 카운트
        }
        else
        {
            BossSpawnText.text = "보스가 출현합니다"; //보스 스폰 텍스트
            StartCoroutine(WaitHpBar()); 
            StopCoroutine(WaitHpBar());
        }

        if(CheckSpawnTime <= BossSpawnTime && IsSpawn == false) //보스 스폰 조건
        {
           
            IsSpawn = true;
            IsBoss = true;
            SpawnBoss();
        }

        TimeCheck += Time.deltaTime;
        if(SpawnTime < TimeCheck && IsBoss == false) //적 스폰 조건
        {
             
           TimeCheck = 0;
           Spawn();
        }
       if(Boss.activeSelf == false)
        {
            BossHpBar.SetActive(false);
        }
    }


    IEnumerator WaitHpBar() //보스 체력바 활성화
    {
        yield return new WaitForSeconds(4.0f);
        BossHpBar.gameObject.SetActive(true);
    }
    private void Spawn() //적 a,b 랜덤 스폰
    {
        Camera mainCam = Camera.main;
        Vector3 limit = mainCam.ViewportToWorldPoint(new Vector3(0, 0.9f, 0)); 
        RandomPosA = new Vector3(10, Random.Range(-limit.y, limit.y), 0);
        RandomPosB = new Vector3(10, Random.Range(-limit.y, limit.y), 0);
        Instantiate(EnemyA, RandomPosA, Quaternion.Euler(0,0,270), StoreEnemy);
        Instantiate(EnemyB, RandomPosB, Quaternion.Euler(0,0,270), StoreEnemy);
    }

    private void SpawnBoss() //보스 스폰
    {
        Instantiate(Boss, Pos, Quaternion.Euler(0, 0, 270));
    }
}
