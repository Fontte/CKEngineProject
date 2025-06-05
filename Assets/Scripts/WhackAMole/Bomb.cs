using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color originalColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = Color.white; // 기본색
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        StartCoroutine(ExplosionFlash());
        ScoreManager.Instance.AddScore(-100);

        if(GameManager.Instance.IsGameOver() == true)
        {
            ScoreManager.Instance.AddScore(0);
        }
    }

    IEnumerator ExplosionFlash()
    {
        spriteRenderer.color = new Color(1f, 0.4f, 0f, 0.8f); // 주황 + 약간 투명
        yield return new WaitForSeconds(0.3f);
       
        spriteRenderer.color = originalColor;
        gameObject.SetActive(false);
    }
}
