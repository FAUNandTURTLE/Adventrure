using UnityEngine;

public class ulitka_muve : MonoBehaviour
{

    public float speed;
    public Vector2 direction;
    public int attackRadius;
    public float delayKus;
    public float damage;
    public LayerMask groundMask;

    private float jumpForce = 50;
    private bool onGround = true;

    public Collider2D attackZone;
    private Collider2D playerCollider;

    private Rigidbody2D fisik;
    private Transform player;

    private Health HPscript;


    void Start()
    {
        direction = Vector2.right;
        fisik = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();

        HPscript = GetComponent<Health>();
    }

    void Update()
    {

        transform.localScale = new Vector2(direction.x, 1);

        Jump(); //прыжки если рядом стена
    }

    public void Attack()
    {
        if (attackZone.IsTouching(playerCollider))
            player.GetComponent<HeroKnight>().TakeDamage(damage);
    }

    public void ChangeDirection()
    {
        if (transform.position.x > player.position.x)
            direction = Vector2.left;
        else if (transform.position.x < player.position.x)
            direction = Vector2.right;

    }// напровляет к ихроку

    private void Jump()
    {
        var hits = Physics2D.RaycastAll(transform.position, direction, 1, groundMask);


        if (hits.Length > 1 && onGround == true && HPscript.dead == false)
        {
            onGround = false;
            fisik.AddForce(Vector2.up * jumpForce + direction, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = true;
    }
}
