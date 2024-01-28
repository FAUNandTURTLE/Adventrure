using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] float m_damage;
    [SerializeField] float m_forceTolchok;
    [SerializeField] Slider HP_BAR;
    [SerializeField] TMP_Text HP_TEXT;
    [SerializeField] GroundCheck m_groundSensor;
    public float Health = 100;

    public Dictionary<string, int> inventory = new();


    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    private TriggerEnter attackZone;
    private bool dead = false;
    public float blockPerscent;
    private bool block;
    public float deadEyes;

    public UnityEvent questComplete;
    public TMP_Text eyeTextInfo;
    public TMP_Text moneyText;
    public float money;

    public Joystick joystick;

    public bool MobileControl = false;

    private bool isAttack = false;
    private bool isJump = false;
    private bool isShieldDown = false;
    private bool isShieldUp = false;

    public GameObject mobilePanel;
    // Use this for initialization
    void Start ()
    {
        deadEyes = 0;
        attackZone = GetComponentInChildren<TriggerEnter>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        inventory["bleb"] = 0;
        inventory["lapki"] = 0;

        MobileControl = Yandex.Instance.isMobile || false;
        mobilePanel.SetActive(MobileControl);
    }

    public void AddMoney(float value)
    {
        money += value;
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update ()
    {
        float inputX;

        if (MobileControl == false)
        {
            isAttack = Input.GetMouseButtonDown(0);
            isJump = Input.GetKeyDown("space");
            isShieldDown = Input.GetMouseButtonDown(1);
            isShieldUp = Input.GetMouseButtonUp(1);
            inputX = Input.GetAxis("Horizontal");
        }
        else
        {
            inputX = joystick.Horizontal;
        }

        m_grounded = m_groundSensor.onGround;

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
         m_animator.SetBool("Grounded", m_grounded);

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            transform.localScale = new Vector2(1, 1);
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling )
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide

        //Attack
        if(isAttack && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            if (attackZone.enemyGameObject != null)
            {
                attackZone.enemyGameObject.GetComponent<Health>().TakeDamage(m_damage);
            }


            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (isShieldDown && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            block = true;
        }

        else if (isShieldUp)
        {
            block = false;
        }
       

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }

        

        //Jump
        else if (isJump && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }

        if (m_body2d.velocity.x != 0 && m_body2d.velocity.y == 0)
        {
            //играть звук травы
        }
        m_animator.SetBool("IdleBlock", block);

        if (MobileControl == true)
        {
            isAttack = false;
            isJump = false;
            isShieldUp = false; 
            isShieldDown = false;
        }
    }

    // Animation Events
    // Called in slide animation.

    public void TakeDamage(float damage) { 
        if (dead == false)
        {
            if (block)
            {
                Health -= damage * (1 - blockPerscent / 100);
            }
            else
            {
                Health -= damage;
            }
           
            HP_BAR.value = Health;
            HP_TEXT.text = Mathf.FloorToInt(Health).ToString();
            m_animator.SetTrigger("Hurt");
            if (Health <= 0)
            {
                dead = true;
                m_animator.SetTrigger("Death");
                this.enabled = false;
            }
        }
    }

    public void ShieldUP()
    {
        isShieldUp = true;
    }

    public void ShieldDown()
    {
        isShieldDown = true;
    }

    public void AttackClick()
    {
        isAttack = true;
    }

    public void JumpClick()
    {
        isJump = true;
    }

    public void AddDiedEyes()
    {
        deadEyes++;
        eyeTextInfo.text = "Убитых глаз: " + deadEyes + " из " + 10;
        print("убитых гусей: " + deadEyes);
        if (deadEyes == 10)
        {
            questComplete.Invoke();
        }
    }
}
