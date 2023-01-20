using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball prefab;
    public void SpawnBall()
    {
        Instantiate(prefab);
    }
}
