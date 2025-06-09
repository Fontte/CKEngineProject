using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Vector2 movedir;
    [SerializeField] float moveSpeed;
    [SerializeField] float Hp;
    [SerializeField] float MaxHp;

    [SerializeField] Image[] HpHearts; // 하트 UI
    [SerializeField] Sprite Heart_Full;
    [SerializeField] Sprite Heart_Half;
    [SerializeField] Sprite Heart_Empty;

    [SerializeField] GameObject GameOver;
    float CheckBulletTime = 0;
    float BulletTime = 0.28f;
    Animator animator;
    public GameObject bullet_prefeb;
    [SerializeField] Transform Player_Muzzle;
    [SerializeField] Transform BulletGroup;

    void Start()
    {
        Hp = MaxHp;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateHpUI(); // 하트 체력 업데이트

        CheckBulletTime += Time.deltaTime;
        movedir.y = Input.GetAxisRaw("Vertical") * moveSpeed;
        movedir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        Shoot();
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = movedir;
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (CheckBulletTime > BulletTime)
            {
                CheckBulletTime = 0;
                CreateBullet(Player_Muzzle.position);
            }
        }
    }

    void CreateBullet(Vector2 pos)
    {
        Bullet bullet = Instantiate(bullet_prefeb, Player_Muzzle.position, Quaternion.Euler(new Vector3(0, 0, 270.0f)), BulletGroup).GetComponent<Bullet>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyA")
        {
            animator.SetTrigger("IsDmg");
            Hp -= 5;
        }
        else if (other.tag == "EnemyB")
        {
            animator.SetTrigger("IsDmg");
            Hp -= 5;
        }
        else if (other.tag == "Boss")
        {
            Hp = 0;
        }

        if (Hp <= 0)
        {
            moveSpeed = 0;
            animator.SetTrigger("IsDie");
            StartCoroutine(WaitDie());
        }
    }

    public void GetHit(float E_dmg)
    {
        animator.SetTrigger("IsDmg");
        Hp -= E_dmg;

        if (Hp <= 0)
        {
            moveSpeed = 0;
            animator.SetTrigger("IsDie");
            StartCoroutine(WaitDie());
        }
    }

    IEnumerator WaitDie()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
        GameOver.SetActive(true);
        Time.timeScale = 0;
    }

    void UpdateHpUI()
    {
        Debug.Log("현재 HP: " + Hp);

        float hpPerHeart = MaxHp / HpHearts.Length;
        for (int i = 0; i < HpHearts.Length; i++)
        {
            float heartMin = i * hpPerHeart;
            float heartMax = (i + 1) * hpPerHeart;

            if (Hp >= heartMax)
            {
                HpHearts[i].sprite = Heart_Full;
            }
            else if (Hp >= heartMin + hpPerHeart / 2f)
            {
                HpHearts[i].sprite = Heart_Half;
            }
            else
            {
                HpHearts[i].sprite = Heart_Empty;
            }
        }
    }

}
