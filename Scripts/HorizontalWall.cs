using UnityEngine;

public class HorizontalWall : Wall
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Change()
    {
        size.y = RandomHeight();
        spriteRenderer.size = size;
        boxCollider.size = size;

        float yPosition = (10 - size.y) / -2f;
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }

    protected override void MoveSpeed()
    {
        base.MoveSpeed();
    }

    protected override void Update()
    {
        MoveSpeed();
    }
}