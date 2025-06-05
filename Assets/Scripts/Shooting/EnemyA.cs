using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    Animator animator;
    public float EnemyAHp;
    
    public float EnemyASpeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
   

    void Update()
    {
        MoveEnemy();
        
    }

    private void MoveEnemy() //적 이동
    {
        transform.position -= transform.up * Time.deltaTime * EnemyASpeed;
    }

    public void OnHit(float _damage) //데미지 처리
    {
        animator.SetTrigger("AHit");
       
        EnemyAHp -= _damage;
        if (EnemyAHp <= 0)
        {
           
            animator.SetTrigger("ADie");
            StartCoroutine(WaitDie());
        }
    }
    IEnumerator WaitDie() //사망 처리
    {
        yield return new WaitForSeconds(0.3f);
        
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) //파괴 처리
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Destroy")
        {
            Destroy(gameObject);
        }
    }
}
