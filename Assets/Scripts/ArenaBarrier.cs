using UnityEngine;
public class VoidGuard : MonoBehaviour
{
    public Transform respawnPoint;   
    public float respawnHeight = 2f;  
    public float killY = -15f;         
    public float checkInterval = 0.25f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < checkInterval) return;
        timer = 0f;

        CheckPlayer();
        CheckEnemies();
    }

    void CheckPlayer()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p == null || p.transform.position.y > killY) return;

        Vector3 target = respawnPoint != null ? respawnPoint.position : Vector3.zero;
        target.y += respawnHeight;

        CharacterController cc = p.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        p.transform.position = target;
        if (cc != null) cc.enabled = true;

        Debug.Log("VoidGuard: Player caiu e foi devolvido ao centro");
    }

    void CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (e.transform.position.y < killY)
            {
                Debug.Log("VoidGuard: zumbi caiu e foi removido");
                Destroy(e);
            }
        }
    }
}