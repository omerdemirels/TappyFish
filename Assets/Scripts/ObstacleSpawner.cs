using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    float timer;
    public float maxTime;
    public float maxY;
    public float minY;
    float randomY;

    public void InstantiateObstacle()
    {
        randomY = Random.Range(minY, maxY);
        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = new Vector2(transform.position.x, randomY);
    }
    void Start()
    {
        //InstantiateObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver == false)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                InstantiateObstacle();
                timer = 0;
            }

        }
    }
}
