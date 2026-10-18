using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 25f;
    float damage = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.CompareTag("Enemy"))
        {
            // Tenta pegar o componente Enemy no objeto colidido ou nos pais dele
            Enemy inimigo = col.gameObject.GetComponentInParent<Enemy>();

            // Se encontrou o script, aplica o dano de forma segura
            if (inimigo != null)
            {
                inimigo.TakeDamage(damage);
            }   
        }
    }
}