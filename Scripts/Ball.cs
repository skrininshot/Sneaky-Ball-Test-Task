using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float forceIncrease;
    [SerializeField] private float forceIncreaseSeconds;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Start()
    {
        Jump();
        StartCoroutine(TimerAddForce());
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
        rb.velocity = new Vector2(0, Mathf.Clamp(rb.velocity.y + jumpForce, -Mathf.Infinity, jumpForce));
    }

    private IEnumerator TimerAddForce()
    {
        while (true)
        {
            WaitForSeconds wait = new(forceIncreaseSeconds);
            yield return wait;
            jumpForce += forceIncrease;
        }
    }

    private void Fail()
    {
        StopCoroutine(TimerAddForce());
        GameUI gameUI = FindObjectOfType<GameUI>();
        if (gameUI == null) return;
        gameUI.gameOver.Invoke();
    }
}
