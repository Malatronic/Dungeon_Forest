using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public float followZoneRadius = 0.7f;
    public float followDuration = f;

    public Animator animator;

    private bool isChasingPlayer;
    private float timeStartedChasing;
    private Vector2 lastPosition;


    void Start()
    {
        animator = GetComponent<Animator>();
        isChasingPlayer = false;
        timeStartedChasing = 0f;
        lastPosition = transform.position;
    }


    void Update()
    {
        Vector2 direction = Player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= followZoneRadius)
        {
            isChasingPlayer = true;
            timeStartedChasing = Time.time;

            direction.Normalize();
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

            animator.SetBool("IsMoving", true);
        }
        else if (isChasingPlayer)
        {
            if (Time.time - timeStartedChasing <= followDuration)
            {
                direction.Normalize();
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

                animator.SetBool("IsMoving", true);
            }
            else
            {
                isChasingPlayer = false;
                animator.SetBool("IsMoving", false);
            }
        }


        if ((Vector2)transform.position != lastPosition)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        lastPosition = transform.position;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followZoneRadius);
    }
}