using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float bulletDmg = 3;
    public float bulletSpeed = 5;

   
    // Update is called once per frame
    

    void Update()
    {
        moveBullet();
        
    }
   
    private void moveBullet()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed; //현재 위치 기준으로 발사하게 해줌.
    }

    private void OnTriggerEnter2D(Collider2D other) //피격 처리
    {
        if(other.tag == "EnemyA")
        {
            EnemyA enemya = other.GetComponent<EnemyA>();
            enemya.OnHit(bulletDmg);
            Destroy(gameObject);
            
        }
        if (other.tag == "EnemyB")
        {
            EnemyB enemyb = other.GetComponent<EnemyB>();
            enemyb.OnHit(bulletDmg);
            Destroy(gameObject);
            
        }

        if (other.tag == "Boss")
        {
            Boss boss = other.GetComponent<Boss>();
            boss.OnHit(bulletDmg);
            Destroy(gameObject);
            ;
        }

        if (other.tag == "CheckCo")
        {
            Destroy(gameObject);
            // 이 태그가 붙어진 곳과 트리거충돌하면 총알이 사라지게된다.
            // 카메라 바깥에서 생성된 적들에게 총알이 맞지 않게하기위함.
        }
    }
}
