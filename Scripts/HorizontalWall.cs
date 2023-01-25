using UnityEngine;

public class HorizontalWall : Wall
{
    private bool jumped = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateWall();
    }

    public override void UpdateWall()
    {
        size.y = RandomHeight();
        spriteRenderer.size = size;
        boxCollider.size = size;

        float yPosition = (10 - size.y) / -2f;
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        jumped = false;
    }

    protected override void MoveSpeed()
    {
        base.MoveSpeed();

        if (jumped) return;
        if (transform.position.x < Ball.Instance.transform.position.x)
        {
            jumped = true;
            GameManager.Instance.ChangeScore.Invoke();
        }
    }

    protected override void Update()
    {
        MoveSpeed();
    }
}