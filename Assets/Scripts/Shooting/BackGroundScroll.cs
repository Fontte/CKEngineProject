using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform part1;
        public Transform part2;
        public float speed;
    }

    [SerializeField] private ParallaxLayer front;
    [SerializeField] private ParallaxLayer mid;
    [SerializeField] private ParallaxLayer behind;

    private float spriteWidth;

    private void Start()
    {
        // 가정: 모든 레이어 오브젝트가 같은 스프라이트 폭을 가짐
        spriteWidth = front.part1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        ScrollLayer(front);
        ScrollLayer(mid);
        ScrollLayer(behind);
    }

    private void ScrollLayer(ParallaxLayer layer)
    {
        Move(layer.part1, layer.speed);
        Move(layer.part2, layer.speed);

        // 왼쪽으로 벗어나면 오른쪽 끝으로 보내기 (무한 루프)
        if (layer.part1.position.x <= -spriteWidth)
        {
            layer.part1.position += Vector3.right * spriteWidth * 2f;
        }
        if (layer.part2.position.x <= -spriteWidth)
        {
            layer.part2.position += Vector3.right * spriteWidth * 2f;
        }
    }

    private void Move(Transform t, float speed)
    {
        t.position += Vector3.left * speed * Time.deltaTime;
    }
}
