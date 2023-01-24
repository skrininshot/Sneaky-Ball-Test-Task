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
    protected bool jumped = false;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        halfWidth = size.x * 0.5f;
    }

    protected virtual void OnEnable()
    {
        spawner = GetComponentInParent<WallSpawner>();
        end = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        UpdateWall();
    }

    public abstract void Change();

    protected virtual void MoveSpeed()
    {
        transform.position -= new Vector3(spawner.Speed * Time.deltaTime, 0);

        if (transform.position.x + halfWidth < end.x)
        {
            jumped = false;
            spawner.Respawn(this);
            UpdateWall();
        }

        if (jumped) return;
        if (transform.position.x < Ball.Instance.transform.position.x)
        {
            jumped = true;
            GameManager.Instance.ChangeScore.Invoke();
        }
    }

    public virtual void UpdateWall()
    {
        jumped = false;
        Change();
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
