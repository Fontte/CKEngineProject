using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float EBullet_Dmg;
    public float EBullet_Speed;
    
    
    

    
    private void OnBecameInvisible() //화면밖으로 나가면 총알 파괴
    {
       Destroy(gameObject);
    }


    void Update()
    {
        moveEBullet();
    }
     
    private void moveEBullet()
    {
        transform.position += transform.right * Time.deltaTime * EBullet_Speed; 
        
    }
    

    private void OnTriggerEnter2D(Collider2D other)  //피격 처리
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.GetHit(EBullet_Dmg);
            Destroy(gameObject);
           
        }
    }
}
