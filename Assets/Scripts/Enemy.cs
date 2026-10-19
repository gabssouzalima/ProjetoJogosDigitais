using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f;
    public Transform player;
    public float purseDistance = 1000f;
    public float attackDistance = 2.5f;
    public float speed = 3f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    private Rigidbody rb;
    private Animator anim;
    private Player playerScript;
    private float nextAttackTime;
    private bool morto = false;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        player = p.transform;
        playerScript = p.GetComponent<Player>();
        if (playerScript == null)
            Debug.LogError("Enemy: o objeto com tag Player não tem o script Player");

        anim = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void FixedUpdate()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        bool isInPursueRange = distToPlayer <= purseDistance;
        bool isInAttackRange = distToPlayer <= attackDistance;

        // direção horizontal até o player
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.001f)
            rb.MoveRotation(Quaternion.LookRotation(dir));

        Vector3 vel = rb.linearVelocity; // mantém o Y para a gravidade continuar funcionando

        if (isInAttackRange)
        {
            anim.SetBool("IsAttacking", true);
            rb.linearVelocity = new Vector3(0f, vel.y, 0f);

            if (Time.time >= nextAttackTime && playerScript != null)
            {
                nextAttackTime = Time.time + attackCooldown;
                Debug.Log("Zumbi atacou o player");
                playerScript.TakeDamage(attackDamage);
            }
        }
        else if (isInPursueRange)
        {
            anim.SetBool("IsAttacking", false);
            Vector3 move = dir.normalized * speed;
            rb.linearVelocity = new Vector3(move.x, vel.y, move.z);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (morto) return;

        hp -= dmg;
        Debug.Log("Zumbi levou dano, hp = " + hp);

        if (hp <= 0)
        {
            morto = true;
            if (KillCounter.Instance != null)
                KillCounter.Instance.AdicionarKill();
            Destroy(gameObject);
        }
    }
}