using UnityEngine;

public class Fish : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private int _angle;
    [SerializeField] private int _maxAngle = 20;
    [SerializeField] private int _minAngle = -60;
    public Score score;
    public GameManager gameManager;
    public Sprite fishDied;
    SpriteRenderer sp;
    Animator anim;
    public ObstacleSpawner obstacleSpawner;
    [SerializeField] private AudioSource swim, hit, point;


    bool touchedGround;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();


    }

    void Update()
    {
        FishSwim();
    }
    private void FixedUpdate()
    {

        FishRotation();
    }
    void FishSwim()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.gameOver == false)
        {
            swim.Play();
            if (GameManager.gameStarted==false)
            {
                _rb.gravityScale = 4f;
                _rb.velocity = Vector2.zero;
                _rb.velocity = new Vector2(_rb.velocity.x, _speed);
                obstacleSpawner.InstantiateObstacle();
                gameManager.GameHasStarted();

            }
            else
            {
            _rb.velocity = Vector2.zero;
            _rb.velocity = new Vector2(_rb.velocity.x, _speed);

            }
        }

    }
    void FishRotation()
    {
        if (_rb.velocity.y > 0)
        {
            if (_angle <= _maxAngle)
            {
                _angle = _angle + 4;
            }
        }
        else if (_rb.velocity.y < -2.5f)
        {
            if (_angle > _minAngle)
            {
                _angle = _angle - 2;
            }
        }
        if (touchedGround == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            score.Scored();
            point.Play();
        }
        else if (collision.CompareTag("Column") && GameManager.gameOver == false)
        {
            gameManager.GameOver();
            FishDieEffect();
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gameOver == false)
            {
                gameManager.GameOver();
                GameOver();
                FishDieEffect();

            }
        }
    }
    void FishDieEffect()
    {
        hit.Play();
    }
    void GameOver()
    {
        touchedGround = true;
        sp.sprite = fishDied;
        anim.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
