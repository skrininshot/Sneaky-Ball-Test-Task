using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Wall : MonoBehaviour
{
    [SerializeField] protected Vector2 size;
    protected Vector2 end;
    protected float halfWidth;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider;
    protected WallSpawner spawner;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        halfWidth = size.x * 0.5f;
    }

    protected virtual void Start()
    {
        spawner = GetComponentInParent<WallSpawner>();
        end = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Change();
    }

    public abstract void Change();

    protected virtual void MoveSpeed()
    {
        transform.position -= new Vector3(spawner.Speed * Time.deltaTime, 0);

        if (transform.position.x + halfWidth < end.x)
        {
            spawner.Respawn(this);
            Change();
        }
    }

    protected virtual int RandomHeight()
    {
        return Random.Range(spawner.MinHeight, spawner.MaxHeight);
    }

    protected virtual void Update()
    {
        MoveSpeed();
    }
}
