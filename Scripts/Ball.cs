using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public static Ball Instance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScaleMultiplier;
    private Rigidbody2D rb;
    private float speed;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.ChangeSpeed.AddListener(ChangeSpeed);
    }

    public void Start()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        Jump();
    }

    private void Update()
    {
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
        return 
            Input.GetMouseButtonDown(0) || 
            Input.GetKeyDown(KeyCode.Space) || 
            Input.GetKeyDown(KeyCode.UpArrow) || 
            Input.GetKeyDown(KeyCode.W);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(0, Mathf.Clamp(rb.velocity.y + jumpForce * speed, -Mathf.Infinity, jumpForce * speed));
    }

    private void Fail()
    {
        gameObject.SetActive(false);
        GameManager gameUI = FindObjectOfType<GameManager>();

        if (gameUI == null) return;
        gameUI.GameEnd.Invoke();
    }

    private void ChangeSpeed(float speed)
    {
        rb.gravityScale = speed * gravityScaleMultiplier;
        this.speed = speed;
    }
}
