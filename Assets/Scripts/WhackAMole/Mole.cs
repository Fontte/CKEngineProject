using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour
{
    [SerializeField] Sprite MoleSprite;
    [SerializeField] Sprite hitMoleSprite;

    SpriteRenderer spriteRenderer;
    bool isHit = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // 등장할 때 초기화
        spriteRenderer.sprite = MoleSprite;
        isHit = false;
    }

    public void Hit()
    {
        if (isHit) return;

        isHit = true;
        spriteRenderer.sprite = hitMoleSprite;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(50);
        }

        if (GameManager.Instance.IsGameOver() == true)
        {
            ScoreManager.Instance.AddScore(0);
        }
        StartCoroutine(DieAfterDelay(0.3f));
    }

    IEnumerator DieAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // OnEnable에서 다시 초기화됨
    }

    void OnMouseDown()
    {
        Hit();
    }
}
