using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        LOOKFOR,
        GOTO,
        ATTACK,
    }
    public State CurState;
    public float Speed = 3;
    public float GoToDistance = 6;
    public float AttackDistance = 2;
    public float AttackTimer = 2;
    private Transform Target;
    public string PlayerTag = "Player";
    private float CurTime;
    private PlayerMovement PlayerScript;
    public HealthBar healthBar;                   //Include HealthBar
    public Player player;                          //Included player class
    [SerializeField] public Transform teleportDestination;
    [SerializeField] public GameObject playerChar;

    // Start is called before the first frame update
    [System.Obsolete]
    IEnumerator Start()
    {              
        Target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        CurTime = AttackTimer;
        if (Target != null)
        {
            PlayerScript = Target.GetComponent<PlayerMovement>();
        }

        while (true)
        {
            switch (CurState)
            {
                case State.LOOKFOR:
                    LookFor();
                    break;
                case State.GOTO:
                    GoTo();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
            yield return 0;
        }
    }

    public void LookFor()
    {
        transform.LookAt(Target);
        if (Vector3.Distance(Target.position, transform.position) < GoToDistance)
        {
            CurState = State.GOTO;            
        }
    }

    public void GoTo()
    {       
        transform.LookAt(Target);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit Buddy;
        if (Physics.Raycast(transform.position, fwd, out Buddy))
        {
            if (Buddy.transform.tag != PlayerTag)
            {
                CurState = State.LOOKFOR;
                return;
            }
        }

        if (Vector3.Distance(Target.position, transform.position) > GoToDistance)
        {
            CurState = State.LOOKFOR;
            return;
        }


        if (Vector3.Distance(Target.position, transform.position) > AttackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        else
        {
            CurState = State.ATTACK;
        }
    }

    [System.Obsolete]
    public void Attack()
    { 
        transform.LookAt(Target);
        CurTime -= Time.deltaTime;
        if (CurTime < 0)
        {
            if (healthBar.health > 0)
            {
                healthBar.health -= 10;
                healthBar.takeDamage(10);
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    healthBar.health = 100;
                    healthBar.SetHealth(100);
                    playerChar.transform.position = teleportDestination.position;
                }
                else
                {
                    Application.LoadLevel(Application.loadedLevel);
                    healthBar.SetHealth(100);
                    healthBar.health = 100;
                }
                
            }
            CurTime = AttackTimer;
        }

        if (Vector3.Distance(Target.position, transform.position) > AttackDistance)
        {
            CurState = State.GOTO;
        }
    }
}

//ANIMATION

//Animator animator;    

//animator = GetComponent<Animator>();                        //Animator created for Drone
//isHitHash = Animator.StringToHash("isHit");
//isFowardHash = Animator.StringToHash("isFoward");
//isFoward = Animator.           //Link animation to variable

//bool isFoward = animator.GetBool(isFowardHash);         //Boolean variable for Animation
//bool isHit = animator.GetBool(isHitHash);

//if (!isFoward)                                      //Foward Animation for Drone
//{
//    animator.SetBool(isFowardHash, true);           //Move Foward Animate
//}
//else if (isFoward)
//{
//    animator.SetBool(isFowardHash, false);          //Stop Moving Foward Animate
//}