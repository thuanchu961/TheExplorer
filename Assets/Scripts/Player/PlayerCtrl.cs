using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Singleton<PlayerCtrl>
{
    [SerializeField] CapsuleCollider2D capColi;
    [SerializeField] BoxCollider2D boxColi;
    [SerializeField] float speed, jumpPower, way, oldSpeed, gravityScale = 10;
    Vector2 movement;
    float horizontal, vertical;
    [SerializeField] Rigidbody2D rb2;
    [SerializeField]
    bool isGround, isJump = false, isMove = true, isCrouch = false, isStaffActtack = false,
     isShootActackState = false, isGroundThrough = false, isLossHealth = false, isHurt = false,
     isDeath = false;
    public bool _isGround => isGround;
    public bool _isGroundThrough => isGroundThrough;
    public bool _isStaffActtack => isStaffActtack;
    public bool _isShootActackState => isShootActackState;
    public bool _isDeath => isDeath;
    [SerializeField] bool staffActackState = false, run = false;
    [SerializeField] AnimCtrlBase anims;
    Vector2 rootPlayerPos;
    SceneCtrl sceneCtrl;
    [SerializeField] float timeJump, lastTimeJump = 0;
    [SerializeField] float timeActiveTrigger, lastTimeActiveTrigger = 0;
    [SerializeField] float timeStaffAttack, lastTimeStaffAttack = 0;
    [SerializeField] float timeLossHealth, lastTimeLossHealth = 0;
    [SerializeField] float timeEffectLossHealth, lastTimeEffectLossHealth = 0;
    float maxJump = 10.5f, maxSpeed = 5.5f, rootGravity, tempVelocityY = 0;
    int boxHealth, fullHealth = 5;
    [SerializeField] SpriteRenderer spriteRenderer;
    Vector2 currentPos;
    float totalGravityScale = 0;
    [SerializeField] GameObject audioStaff;

    // Start is called before the first frame update
    void Start()
    {
        speed = 350;
        jumpPower = 560;
        sceneCtrl = SceneCtrl._inst_singleton;
        timeJump = 2;
        timeStaffAttack = 4;
        timeLossHealth = 10;
        timeEffectLossHealth = 0.8f;
        rootGravity = rb2.gravityScale;
        rootPlayerPos = transform.position;
        if (PlayerPrefs.HasKey("boxHealth"))
            boxHealth = PlayerPrefs.GetInt("boxHealth");
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneCtrl._sceneLoad.activeSelf)
            return;

        if (!sceneCtrl._paused)
            return;

        ChangeAnim();

        if (PlayerPrefs.HasKey("boxHealth"))
        {
            boxHealth = PlayerPrefs.GetInt("boxHealth");
            if (boxHealth <= 0)
            {
                List<GameObject> l = ObjectPooling._inst_singleton.TMT_GetActivePlayerBullet();
                foreach (var i in l)
                {
                    i.SetActive(false);
                }
                spriteRenderer.enabled = true;
                isDeath = true;
                PlayerPrefs.SetInt("boxHealth", fullHealth);
                boxHealth = PlayerPrefs.GetInt("boxHealth");
                sceneCtrl.TMT_CallLoadScene(5);
            }
        }

        way = transform.localScale.x;

        isGround = GroundCheck._inst_singleton._isGround;
        isGroundThrough = GroundCheck._inst_singleton._isGroundThrough;

        lastTimeJump = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeJump);
        lastTimeStaffAttack = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeStaffAttack);
        lastTimeEffectLossHealth = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeEffectLossHealth);
        if (run)
            lastTimeLossHealth = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeLossHealth);

        if (isDeath)
        {
            rb2.gravityScale = 20;
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        CommonFunc._inst_singleton.TMT_CheckStaffState();

        transform.localScale = CommonFunc._inst_singleton.TMT_ChangeLocalScale(horizontal, transform.localScale);

        Moving();
        Jumping();
        Crouching();

        if (Input.GetKey(KeyCode.S))
        {
            capColi.enabled = false;
            boxColi.enabled = true;
        }
        else
        {
            capColi.enabled = true;
            boxColi.enabled = false;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.J))
        {
            if (isGroundThrough)
            {
                boxColi.isTrigger = true;
                rb2.gravityScale = gravityScale;
                lastTimeActiveTrigger = 0.9f;
            }
        }

        lastTimeActiveTrigger = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeActiveTrigger);

        if (lastTimeActiveTrigger > 0)
            return;

        if (isGround)
        {
            boxColi.isTrigger = false;
            rb2.gravityScale = rootGravity;
        }

        if (Input.GetKeyDown(KeyCode.L) && staffActackState)
            isShootActackState = true;
        else if (Input.GetKeyUp(KeyCode.L) || !staffActackState)
            isShootActackState = false;
    }

    void Moving()
    {
        if (!isMove)
        {
            rb2.velocity = Vector2.zero;
            return;
        }
        if (isCrouch)
            return;

        movement.x = horizontal * speed * Time.deltaTime;
        movement.y = rb2.velocity.y;

        rb2.velocity = movement;

        if (rb2.velocity.y > maxJump)
            rb2.velocity = new Vector2(rb2.velocity.x, maxJump);
        if (rb2.velocity.x > maxSpeed)
            rb2.velocity = new Vector2(maxSpeed, rb2.velocity.y);
        if (rb2.velocity.x < -maxSpeed)
            rb2.velocity = new Vector2(-maxSpeed, rb2.velocity.y);
    }

    void Jumping()
    {
        if (!isMove)
            return;
        if (isCrouch)
            return;

        if (rb2.velocity.y > tempVelocityY)
        {
            tempVelocityY = rb2.velocity.y;
        }
        else if (rb2.velocity.y < tempVelocityY)
        {
            totalGravityScale = rb2.gravityScale;
            tempVelocityY = rb2.velocity.y;
            if (totalGravityScale <= 3.5f)
                totalGravityScale += Time.deltaTime * 2.2f;
            rb2.gravityScale = totalGravityScale;
        }

        if (lastTimeJump > 0)
            return;

        if (Input.GetKey(KeyCode.J))
        {
            if (isGround)
                rb2.AddForce(new Vector2(0, jumpPower));

            lastTimeJump = timeJump;
        }
    }

    void ChangeAnim()
    {
        if (isGround)
        {
            if (isDeath)
            {
                anims.playerState = AnimCtrlBase.State.death;
                rb2.velocity = Vector2.zero;
                isMove = false;
                return;
            }
            if (isHurt)
            {
                anims.playerState = AnimCtrlBase.State.hurt;
                return;
            }
            if (Input.GetKey(KeyCode.S))
            {
                anims.playerState = AnimCtrlBase.State.crouch;
                isJump = false;
                rb2.velocity = Vector2.zero;
                if (Input.GetKey(KeyCode.L) && staffActackState)
                    anims.playerState = AnimCtrlBase.State.shoot;
            }
            else if (Input.GetKey(KeyCode.L) && staffActackState && isShootActackState && !isJump)
            {
                anims.playerState = AnimCtrlBase.State.shoot;
                isMove = true;

            }
            else if (Input.GetKey(KeyCode.K) && staffActackState && !isJump)
            {
                if (lastTimeStaffAttack <= 0)
                {
                    anims.playerState = AnimCtrlBase.State.staffAttack;
                    audioStaff.GetComponent<AudioBase>().TMT_RandomAudioClip();
                    isStaffActtack = true;
                    StartCoroutine(AttackStaffTime());
                }
                else
                {
                    if (rb2.velocity.x == 0)
                        anims.playerState = AnimCtrlBase.State.idle;
                    else
                        anims.playerState = AnimCtrlBase.State.run;
                }
            }
            else if (isJump && !isGroundThrough)
                anims.playerState = AnimCtrlBase.State.jumpEnd;
            else if (rb2.velocity.x == 0)
                anims.playerState = AnimCtrlBase.State.idle;
            else
                anims.playerState = AnimCtrlBase.State.run;
        }
        else
        {
            if (isDeath)
            {
                anims.playerState = AnimCtrlBase.State.death;
                rb2.velocity = Vector2.zero;
                isMove = false;
                return;
            }
            anims.playerState = AnimCtrlBase.State.jump;
            isJump = true;
            if (Input.GetKey(KeyCode.L) && staffActackState)
                anims.playerState = AnimCtrlBase.State.shoot;
        }
    }

    public void TMT_SetIsJump(bool jump)
    {
        isJump = jump;
    }

    public void TMT_SetIsMove(bool move)
    {
        isMove = move;
    }

    void Crouching()
    {
        if (isJump)
            return;
        if (Input.GetKey(KeyCode.S))
        {
            isCrouch = true;
            rb2.velocity = Vector2.zero;
        }
        else
            isCrouch = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDeath)
        {
            return;
        }

        if (other.tag == "GatePoint1")
            sceneCtrl.TMT_CallLoadScene(1);
        if (other.tag == "GatePoint2")
            sceneCtrl.TMT_CallLoadScene(2);
        if (other.tag == "GatePoint3")
            sceneCtrl.TMT_CallLoadScene(3);
        if (other.tag == "GatePoint4")
            sceneCtrl.TMT_CallLoadScene(4);
        // if (other.tag == "GatePoint5")
        //     sceneCtrl.TMT_CallLoadScene(5);

        if (other.tag == "Acid")
        {
            GetDmg("Acid");
        }

        if (other.tag == "HealthPickup")
        {
            if (PlayerPrefs.HasKey("boxHealth"))
            {
                PlayerPrefs.SetInt("eventHealth", 1);
                boxHealth = PlayerPrefs.GetInt("boxHealth");
                boxHealth++;
                PlayerPrefs.SetInt("boxHealth", boxHealth);
            }
        }

        if (other.tag == "Enemy")
        {
            GetDmg("Enemy");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDeath)
            return;

        if (other.tag == "Enemy")
        {
            GetDmg("Enemy");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDeath)
        {
            return;
        }

        if (other.collider.tag == "Enemy")
        {
            GetDmg("Enemy");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {

        if (other.collider.tag == "Ground")
            transform.parent = null;

        if (isDeath)
            return;

        if (other.collider.tag == "Enemy")
        {
            GetDmg("Enemy");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "GroundThrough")
            transform.parent = null;
    }

    IEnumerator DelayLossHealth(string ob)
    {
        isHurt = true;
        run = true;
        lastTimeLossHealth = timeLossHealth;
        yield return new WaitUntil(() => lastTimeLossHealth <= (timeLossHealth / 2));
        isHurt = false;
        if (ob == "Acid")
            sceneCtrl.TMT_PlayerIntoAcid();
        yield return new WaitUntil(() => lastTimeLossHealth <= 0);
        isLossHealth = false;
        run = false;
        isHurt = false;
    }

    public void TMT_SetStaffState(bool b)
    {
        staffActackState = b;
    }

    void GetDmg(string ob)
    {
        if (isLossHealth)
            return;

        isLossHealth = true;
        if (PlayerPrefs.HasKey("boxHealth"))
        {
            PlayerPrefs.SetInt("eventHealth", 0);
            boxHealth = PlayerPrefs.GetInt("boxHealth");
            boxHealth--;
            PlayerPrefs.SetInt("boxHealth", boxHealth);
            currentPos = transform.position;
            StartCoroutine(DelayLossHealth(ob));
            StartCoroutine(EffectGetDmg());
        }
    }

    IEnumerator EffectGetDmg()
    {
        if (!isDeath)
        {
            while (lastTimeLossHealth > 0)
            {
                spriteRenderer.enabled = false;
                lastTimeEffectLossHealth = timeEffectLossHealth;
                yield return new WaitUntil(() => lastTimeEffectLossHealth <= 0);
                spriteRenderer.enabled = true;
                lastTimeEffectLossHealth = timeEffectLossHealth;
                yield return new WaitUntil(() => lastTimeEffectLossHealth <= 0);
            }
        }
        yield return new WaitUntil(() => lastTimeLossHealth <= (timeLossHealth / 2));
        isHurt = false;
        yield return new WaitUntil(() => lastTimeLossHealth <= 0);
    }

    public void TMT_SetIsHurt(bool b)
    {
        isHurt = b;
    }

    public void TMT_SetSpeed(bool b)
    {
        if (!b)
        {
            oldSpeed = speed;
            speed = 0;
        }
        else
            speed = oldSpeed;
    }

    IEnumerator AttackStaffTime()
    {
        yield return new WaitForEndOfFrame();
        lastTimeStaffAttack = timeStaffAttack;
        isStaffActtack = false;
    }
}
