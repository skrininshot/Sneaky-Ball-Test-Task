using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public float Speed;
    [SerializeField] private float difficultSpeedMultiplier;
    [SerializeField] private float additiveSpeed;

    [SerializeField] private Wall prefab;
    [Range(0,10)] public int MinHeight;
    [Range(0,10)] public int MaxHeight;
    [SerializeField] private float distance;
    [SerializeField][Range(0,1)] private float shift;
    [SerializeField] private int startDistance;

    private List<Wall> walls = new List<Wall>();
    private Wall lastRespawned;

    private void Awake()
    {
        Vector2 worldLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 worldRight = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, 0));
        float worldLength = Vector2.Distance(worldLeft, worldRight);
        int count = Mathf.CeilToInt(worldLength / distance);

        Speed = additiveSpeed + difficultSpeedMultiplier * (int)GameManager.Instance.SelectedDifficult;

        for (int i = 0; i < count; i++)
        {
            Wall wall = Instantiate(prefab, transform);
            wall.enabled = false;
            walls.Add(wall);
        }

        lastRespawned = walls[walls.Count - 1];
    }

    public void WallsSetEnabled(bool moving)
    {
        foreach(Wall wall in walls)
        {
            wall.enabled = moving;
        }

        if (moving) SetOnStart();
    }

    private void SetOnStart()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            float spawnDistance = startDistance + (distance * shift) + distance * i;
            walls[i].transform.position = new Vector3(spawnDistance, walls[i].transform.position.y);
            walls[i].enabled = true;
        }

        lastRespawned = walls[walls.Count - 1];
    }

    public void Respawn(Wall wall)
    {
        wall.transform.position = lastRespawned.transform.position + new Vector3(distance, 0, 0);
        lastRespawned = wall;
    }
}