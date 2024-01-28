using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public string EnemyTag;
    public GameObject enemyGameObject;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.gameObject.tag== EnemyTag)
        {
            enemyGameObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.gameObject.tag == EnemyTag)
        {
            enemyGameObject = null;
        }
    }
}
