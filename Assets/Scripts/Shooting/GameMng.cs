using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameMng : MonoBehaviour
{

    public static GameMng instance;      //전역변수

    public static GameMng ins
    {

        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<GameMng>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<GameMng>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    
       
    [SerializeField] public GameObject Clear;
    [SerializeField] public EnemyMng enemyMng;

    private void Update()
    {              
        if (SceneManager.GetActiveScene().name == "Shooting")
        {
            if (enemyMng.CheckSpawnTime <= 0)
            {


                if (enemyMng.BossHpFill_Img.fillAmount <= 0)
                {
                    StartCoroutine(GameClear());
                    StopCoroutine(GameClear());
                }
            }
        }
    }
   
    IEnumerator GameClear() //게임클리어
    {
        yield return new WaitForSeconds(0.3f);
        Clear.gameObject.SetActive(true);
        Time.timeScale = 0; 

    }   
}
