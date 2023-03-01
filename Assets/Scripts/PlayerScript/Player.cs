using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int WalkHash, WalkBackHash, RunHash, JumpHash;
    [SerializeField] private float speed = 5f, rot = 3f;
    [SerializeField] private int freeKid;
    float axeHorizontal, axeVertical;

    bool isAbleToRun, isGrounded;

    public static Vector3 lastCheckPointPos = new Vector3(-3,0);

    public int maxHealth = 4;
    public float maxStamina = 100, maxHunger = 100;
    
    public int currentHealth;
    public float currentStamina, currentHunger;

    public HealthBar PlayerHealthBar;
    public StaminaBar PlayerStaminaBar;
    public HungerBar PlayerHungerBar;

    Animator PlayerAnim;

    Rigidbody PlayerRigidBody;
    CharacterController PlayerController;
    AudioSource PlayerAudio;

    EndMenu endMenu;
    CanvasGroup endMenuGroup;

    private void OnCollisionEnter(Collision collision)
    {
        EnemyAi enemy = collision.gameObject.GetComponent<EnemyAi>();
        Chicken food = collision.gameObject.GetComponent<Chicken>();

  

        if(enemy)
        {
            TakeDamage(1);
            if(currentHealth <= 0) 
            {
                endMenuGroup.interactable = true;
                endMenuGroup.alpha = 1f;
                endMenu.EndMenuButton();
            }
        }

        if(food)
        {
            regenHunger(50);
            Destroy(food.gameObject);
        }


        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            isAbleToRun = true;
        }
        if (collision.gameObject.tag == "Head")
         {
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FreeKid kid = other.gameObject.GetComponent<FreeKid>();

        Debug.Log(other.gameObject.name);

        if (kid)
        {
            freeKid += 1;
        }
    }

    private void Awake()
    {

        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
        
        PlayerAnim = GetComponent<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
        PlayerController = GetComponent<CharacterController>();
        PlayerRigidBody = GetComponent<Rigidbody>();

        endMenu = GetComponent<EndMenu>();
        endMenuGroup = GameObject.Find("EndCanvas").GetComponent<CanvasGroup>();

        WalkHash = Animator.StringToHash("walk");
        WalkBackHash = Animator.StringToHash("walk_back");
        RunHash = Animator.StringToHash("running");
        JumpHash = Animator.StringToHash("jump");
    }

    // Start is called before the first frame update
    void Start()
    {
        endMenuGroup.interactable = false;

        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentHunger = maxHunger;

        PlayerHealthBar.SetMaxHealth(maxHealth);
        PlayerStaminaBar.SetMaxStamina(maxStamina);
        PlayerHungerBar.SetMaxHunger(maxHunger);
    }

    // Update is called once per frame
    void Update()
    {
        bool walk = PlayerAnim.GetBool(WalkHash);

        axeHorizontal = Input.GetAxisRaw("Horizontal");
        axeVertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * speed * axeVertical * Time.deltaTime);

        if (freeKid == 3)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0,0);
            endMenuGroup.alpha = 1f;
            endMenuGroup.interactable = true;
        }

        //Getting Hungry
        GettingHungry(1);

        //forward animation
        if (!walk && Input.GetKey(KeyCode.UpArrow))
        {
            PlayerAnim.SetBool(WalkHash, true);
        }
        else if (walk && !Input.GetKey(KeyCode.UpArrow))
        {
            PlayerAnim.SetBool(WalkHash, false);
        }

        //Back animation
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            PlayerAnim.SetBool(WalkBackHash, true);
        }
        else
        {
            PlayerAnim.SetBool(WalkBackHash, false);
        }

        //Move the camera
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rot * 20 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -rot * 20 * Time.deltaTime);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            PlayerRigidBody.AddForce(Vector3.up * 8.5f, ForceMode.Impulse);
            PlayerAnim.SetBool(JumpHash, true);
            isGrounded = false;

            PlayerAnim.SetBool(RunHash, false);
            PlayerAnim.SetBool(WalkBackHash, false);
            PlayerAnim.SetBool(WalkHash, false);
            isAbleToRun = false;
        }
        else if(!Input.GetKeyDown(KeyCode.Space) && isGrounded == false)
        {
            //PlayerAnim.SetBool(JumpHash, false);
        }
        else if(!Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            PlayerAnim.SetBool(JumpHash, false);
        }

        if(currentHunger <= 0)
        {
            endMenuGroup.interactable = true;
            endMenuGroup.alpha = 1f;
            endMenu.EndMenuButton();
        }
    }

    private void FixedUpdate()
    {
        //running
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && isAbleToRun == true)
        {
            PlayerAnim.SetBool(RunHash, true);
            transform.Translate(Vector3.forward * speed * axeVertical * 2 * Time.deltaTime);
            LosingStamina(10);
            if (currentStamina <= 0)
            {
                PlayerAnim.SetBool(RunHash, false);
            }
        }
        else if (!Input.GetKey(KeyCode.UpArrow) || !Input.GetKey(KeyCode.LeftShift))
        {
            PlayerAnim.SetBool(RunHash, false);
            RegenStamina(20);
        }
    }

    void GettingHungry(float hungerpt)
    {
        if (currentHunger >= 0)
        currentHunger -= hungerpt * Time.deltaTime;

        PlayerHungerBar.SetHunger(currentHunger);
    }

    void regenHunger(float regenHunger)
    {

        currentHunger += regenHunger;
        if(currentHunger > 100)
        currentHunger = 100;

        PlayerHungerBar.SetHunger(currentHunger);
    }

    void LosingStamina(float staminaDepleted)
    {
        currentStamina -= staminaDepleted * Time.deltaTime;

        PlayerStaminaBar.SetStamina(currentStamina);
    }

    void RegenStamina(float regenStamina)
    {
        if (currentStamina <= 100)
        currentStamina += regenStamina * Time.deltaTime;

        PlayerStaminaBar.SetStamina(currentStamina);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        PlayerHealthBar.SetHealth(currentHealth);
    }
}
