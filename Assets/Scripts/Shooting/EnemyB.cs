using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    Animator animator;
    
    public float EnemyBHp;
    
    public float EnemyBSpeed;
    public GameObject EBullet_prefeb;
    
    [SerializeField] Transform Enemy_Muzzle;
    Transform EBulletGroup;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EBulletGroup = GameObject.Find("EBulletGroup").transform;
    }
    // Update is called once per frame

   
    void Update()
    {
        
        MoveEnemy();
    }

    private void MoveEnemy() //적 이동
    {
        transform.position -= transform.up * Time.deltaTime * EnemyBSpeed; 
    }

    IEnumerator EnemyAttack() //적 공격
    {
       
        while (true)
        { 
            yield return new WaitForSeconds(1.5f);
            Instantiate(EBullet_prefeb, Enemy_Muzzle.position, Quaternion.Euler(new Vector3(0,0,180)), EBulletGroup).GetComponent<EnemyBullet>();
            yield return null;
        }
    }

   
    
    public void OnHit(float _damage) //데미지 처리
    {
        animator.SetTrigger("BHit");
        
        
        EnemyBHp -= _damage;
        if (EnemyBHp <= 0)
        {
            
            animator.SetTrigger("BDie");
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
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            
        }

        if(other.tag == "Destroy")
        {
            Destroy(gameObject);
           
        }
        
        

        if (other.tag == "CheckCo")
        {
            
            StartCoroutine(EnemyAttack()); //이 태그가 붙여진 곳에 트리거 충돌해야지만 코루틴이 생성된다.
        }
    }
}
