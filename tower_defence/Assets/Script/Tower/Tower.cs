using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    public GameObject bullet;
    public float attackSpeed = 1.0f;

    private List<GameObject> Enemys;
    Transform target = null;


    private void Awake()
    {
        Enemys = new List<GameObject>();
    }

    private void Start()
    {
        StartCoroutine(PeriodAttack());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemys.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            foreach (GameObject enemy in Enemys)
            {
                if (enemy == collision.gameObject)
                {
                    Enemys.Remove(enemy);
                    break;
                }
            }
        }
    }

    private void Attack()
    {
        target = Enemys[0].transform;
        Vector3 targetDir = (target.position - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.right, targetDir);

        Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }

    IEnumerator PeriodAttack()
    {
        while (true)
        {
            if (Enemys.Count != 0)
            {
                Attack();
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
