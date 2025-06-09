using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator animator;

    public float BossHp;
    public float BossMaxHp;

    public float BossSpeed;
    public float BulletTime = 0.2f;
    int CurPattern = 0;
    float CheckBulletTime;
    delegate void BossPattern();
    bool IsBossPattern = false;
    bool IsPattern = false;
    List<BossPattern> bossPattern = new List<BossPattern>();
    [SerializeField] Transform Boss_Muzzle;
   
    [SerializeField] GameObject EBullet_prefeb;
     public Transform EBulletGroup;
    // Start is called before the first frame update
    void Start()
    {


    }


    IEnumerator BossPatternMng()
    {
        while (true)
        {
            CurPattern = Random.Range(0, bossPattern.Count); //랜덤 패턴


            switch (CurPattern)
            {
                case 0:
                    BulletTime = 0.3f; //패턴별 총알 간격
                    break;
                case 1:
                    BulletTime = 0.4f;
                    break;
                case 2:
                    BulletTime = 0.8f;
                    break;
                case 3:
                    BulletTime = 1.0f;
                    break;
                default:
                    break;

            }
            yield return new WaitForSeconds(Random.Range(3.0f, 6.0f)); //패턴 지속 시간(3~6초 랜덤)
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (IsPattern == false)
        {
            animator = GetComponent<Animator>();
            bossPattern.Add(Pattern1); //리스트 안에 패턴들을 추가해준다.
            bossPattern.Add(Pattern2);
            bossPattern.Add(Pattern3);
            bossPattern.Add(Pattern4);

            StartCoroutine(BossPatternMng());
            IsPattern = true;
        }
        GameMng.ins.enemyMng.BossHpFill_Img.fillAmount = BossHp / BossMaxHp;

        CheckBulletTime += Time.deltaTime;
        if (IsBossPattern == true) //보스가 BossStop에 충돌하면 패턴이 시작됨.
        {
            bossPattern[CurPattern]();
        }
        transform.position -= transform.up * Time.deltaTime * BossSpeed;


    }



    private void OnTriggerEnter2D(Collider2D other) //이곳에 충돌처리가 되면 보스가 멈추고 패턴을 시작함.
    {

        if (other.tag == "BossStop")
        {
            BossSpeed = 0;
            IsBossPattern = true;

            BossHp = BossMaxHp;
        }


    }



    void Pattern1() //3개의 총구에서 발사
    {
        if (CheckBulletTime > BulletTime)
        {
            CheckBulletTime = 0;
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 0.8f, 0), Quaternion.Euler(new Vector3(0, 0, 195)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 0.8f, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -0.8f, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -0.8f, 0), Quaternion.Euler(new Vector3(0, 0, 165)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 90), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 270), EBulletGroup).GetComponent<EnemyBullet>();
        }

    }
    void Pattern2() //총구 하나에서 11갈래로 발사(샷건형식)
    {
        if (CheckBulletTime > BulletTime)
        {

            CheckBulletTime = 0;
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 120), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 135), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 150), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 165), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 180), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 195), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 210), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 225), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 240), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 90), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 270), EBulletGroup).GetComponent<EnemyBullet>();

        }
    }
    void Pattern3()
    {
        if (CheckBulletTime > BulletTime) //7개의 총구에서 총알이 나가는 패턴
        {

            CheckBulletTime = 0;
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 1.8f, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 200)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 1.8f, 0), Quaternion.Euler(new Vector3(0, 0, 220)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -1.8f, 0), Quaternion.Euler(new Vector3(0, 0, 140)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 160)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -1.8f, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 90), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 270), EBulletGroup).GetComponent<EnemyBullet>();
        }
    }
    void Pattern4() //총알이 3개의 총구에서 여러각도로 나감
    {
        if (CheckBulletTime > BulletTime)
        {
            CheckBulletTime = 0;
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -1, 0), Quaternion.Euler(new Vector3(0, 0, 150)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -1, 0), Quaternion.Euler(new Vector3(0, 0, 165)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -1, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -1, 0), Quaternion.Euler(new Vector3(0, 0, 195)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, -1, 0), Quaternion.Euler(new Vector3(0, 0, 210)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 150)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 165)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 180)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 195)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position + new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 210)), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 90), EBulletGroup).GetComponent<EnemyBullet>();
            Instantiate(EBullet_prefeb, Boss_Muzzle.position, Quaternion.Euler(0, 0, 270), EBulletGroup).GetComponent<EnemyBullet>();
        }
    }

    public void OnHit(float _damage) //플레이어로부터 데미지를 입음
    {

        animator.SetTrigger("BossHit");
        BossHp -= _damage;
        if (BossHp <= 0)
        {
            StopCoroutine(BossPatternMng());


            animator.SetTrigger("BossDie");
            StartCoroutine(WaitDie());


        }
    }
    IEnumerator WaitDie() //보스 사망
    {
        yield return new WaitForSeconds(0.2f);
       
        
        
        gameObject.SetActive(false);


    }
}
