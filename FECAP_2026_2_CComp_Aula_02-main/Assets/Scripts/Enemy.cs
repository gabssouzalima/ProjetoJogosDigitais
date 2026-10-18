using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f;
    private float distToPlayer;
    private bool isInPursueRange = false;
    private bool isInAttackRange = false;
    public Transform player;
    public float purseDistance = 1000f;
    public float attackDistance = 1.5f;
    public float speed = 3f;

    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        //Debug.Log(disToPlayer);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distToPlayer = Vector3.Distance(transform.position, player.position);

        isInPursueRange = distToPlayer <= purseDistance;
        isInAttackRange = distToPlayer <= attackDistance;
        
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        if (isInAttackRange)
        {
           
            anim.SetBool("IsAttacking", true);
        }
        else if (isInPursueRange)
        {
            anim.SetBool("IsAttacking", false);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
    public void TakeDamage( float dmg)
    { 
    //Debug.Log($"Perdi {dmg} hp");
    hp -= dmg;
        if (hp <= 0)
        {
           Destroy(gameObject);
        }
    }
}

