using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public static Ball Instance;
    [SerializeField] private float jumpForce;
    private float gravitySpeed;
    [SerializeField] private float gravityScaleMultiplier;
    private Rigidbody2D rb;
    private bool freeze = false;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        Freeze();
    }

    public void StartBall()
    {
        Unfreeze();
        transform.position = Vector3.zero;
        Jump();
    }

    private void Update()
    {
        ChangeSpeed(GameManager.Instance.WorldSpeed);
        if (GetInput())
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fail();
    }

    private void OnBecameInvisible()
    {
        Fail();
    }

    private bool GetInput()
    {
        if (freeze) return false;

        return 
            Input.GetMouseButtonDown(0) || 
            Input.GetKeyDown(KeyCode.Space) || 
            Input.GetKeyDown(KeyCode.UpArrow) || 
            Input.GetKeyDown(KeyCode.W);
    }

    private void Jump()
    {
        Debug.Log("Jump: " + rb.velocity);
        rb.velocity = new Vector2(0, Mathf.Clamp(rb.velocity.y + jumpForce * gravitySpeed, -Mathf.Infinity, jumpForce * gravitySpeed));
    }

    private void Fail()
    {
        Freeze();
        GameManager gameUI = FindObjectOfType<GameManager>();

        if (gameUI == null) return;
        gameUI.GameEnd.Invoke();
    }

    private void ChangeSpeed(float speed)
    {
        rb.gravityScale = gravityScaleMultiplier * speed;
        gravitySpeed = speed;
    }

    private void Freeze()
    {
        rb.velocity = Vector2.zero;
        freeze = true;
        rb.Sleep();
    }

    private void Unfreeze()
    {
        freeze = false;
        rb.WakeUp();
    }
}
