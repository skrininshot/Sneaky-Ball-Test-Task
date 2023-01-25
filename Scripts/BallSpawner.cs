using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball prefab;
    private Ball ball;

    private void Start()
    {
        ball = Instantiate(prefab);
    }

    public void StartBall()
    {
        ball.StartBall();
    }
}
