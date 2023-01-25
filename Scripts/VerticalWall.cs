using UnityEngine;

public class VerticalWall : Wall
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void MoveSpeed()
    {
        base.MoveSpeed();
    }

    public override void UpdateWall()
    {
        spriteRenderer.size = size;
        boxCollider.size = size;
        float height = Random.Range(spawner.MinHeight, spawner.MaxHeight);
        transform.position = new Vector3(transform.position.x, height);
    }

    protected override void Update()
    {
        base.Update();
    }
}
