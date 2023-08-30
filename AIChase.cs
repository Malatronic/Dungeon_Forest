using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public float followZoneRadius = 0.7f;
    public float followDuration = 3f; // Number of seconds to follow the player after leaving the radius

    public Animator animator;

    private bool isChasingPlayer;
    private bool isPlayerInRadius;
    private float timeStartedChasing;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isChasingPlayer = false;
        isPlayerInRadius = false;
        timeStartedChasing = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= followZoneRadius)
        {
            isPlayerInRadius = true;

            direction.Normalize();
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

            if (!isChasingPlayer)
            {
                isChasingPlayer = true;
                timeStartedChasing = Time.time;
                animator.SetBool("IsMoving", true);
            }
        }
        else
        {
            if (isPlayerInRadius)
            {
                if (Time.time - timeStartedChasing >= followDuration)
                {
                    isPlayerInRadius = false;
                    isChasingPlayer = false;
                    animator.SetBool("IsMoving", false);
                }
            }
        }
    }
}