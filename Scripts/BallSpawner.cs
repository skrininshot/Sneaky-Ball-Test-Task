using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball prefab;
    private Ball ball;

    private void Start()
    {
        ball = Instantiate(prefab);
        ball.gameObject.SetActive(false);
    }

    public void StartBall()
    {
        ball.gameObject.SetActive(true);
        ball.Start();
    }
}
