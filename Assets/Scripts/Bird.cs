using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private float speed;
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private Animator _animator;
    public RuntimeAnimatorController yellowbird;
    public RuntimeAnimatorController bluebird;
    public RuntimeAnimatorController redbird;
    

    private void Awake()
    {
        if (rigidbody2D == null)
            rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        float randomBird = Random.Range(0, 3);
        switch (randomBird)
        {
            case 0 :
                _animator.runtimeAnimatorController = yellowbird;
                break;
            case 1:
                _animator.runtimeAnimatorController = bluebird;
                break;
            case 2:
                _animator.runtimeAnimatorController = redbird;
                break;
        }

        

    }

    private void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            Move();
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            Move();
#endif
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag("Pipe") || collision2D.collider.CompareTag("Ground"))
        {
            Debug.Log(string.Format("Bird :: OnCollisionEnter2D() :: {0}", collision2D.collider.name));

            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("PipeTrigger"))
        {
            Debug.Log(string.Format("Bird :: OnTriggerEnter2D() :: {0}", collider2D.name));

            GameManager.Instance.IncreaseScore();
        }
    }

    private void Move()
    {
        Debug.Log("Bird :: Move()");

        rigidbody2D.velocity = Vector2.up * speed;
    }

    public void FreezeeBirdPosition()
    {
        Debug.Log("Bird :: FreezeeBirdPosition()");

        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;

        rigidbody2D.AddForce(Vector2.zero);
    }

    public void UnfrezeeBirdPosition()
    {
        Debug.Log("Bird :: UnfrezeeBirdPosition()");

        rigidbody2D.constraints = RigidbodyConstraints2D.None;

        rigidbody2D.AddForce(Vector2.zero);
    }
}
