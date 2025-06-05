using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] float gameTime = 30f;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text timeOutText;

    bool isGameOver = false;
    float currentTime;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentTime = gameTime;
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0f, gameTime);

        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
        }
    }

    void EndGame()
    {
        isGameOver = true;
        timeOutText.text = "Time Out!";
        Time.timeScale = 0;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
