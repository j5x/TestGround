using UnityEngine;
using UnityEngine.Events;

public class PCombat : MonoBehaviour
{
    [SerializeField] UnityEvent OnSuccesFullParry;
    [SerializeField] Animator anim;
    public bool _parry;
    private float waitTime;
    private AnimationClip clip;
    private EnemyScript enemyScript;
    
    public float Radius;
    public bool blocking;
    public float posture;
    private bool postureBroken = false;
    public bool slashing;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Slash();
        Blocking();
        PostureBreak();
        
    }

    private void Blocking()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            blocking = true;
            posture = 100;
            postureBroken = false;
            anim.SetBool("canBlock", true);
            // Start the posture regeneration coroutine
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, Radius, transform.forward, Radius);
            
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if(hit.collider.GetComponent<EnemyScript>().Parried())
                    {
                        OnSuccesFullParry?.Invoke();
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetBool("canBlock", false);
            blocking = false;   
        }
    }

    private void PostureBreak()
    {
        if (posture <= 0)
        {
            blocking = false;
            anim.SetTrigger("isPosture");
            posture = 100;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    private void Slash()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            slashing = true;
            anim.SetTrigger("canSlash");
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, Radius, transform.forward, Radius);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hit.collider.GetComponent<EnemyScript>().Parried())
                    {
                        OnSuccesFullParry?.Invoke();
                    }
                }
            }
        }
    }
    
    private void ParryStart()
    {
        _parry = true;
    }

    private void ParryEnd()
    {
        _parry = false;
    }

    // Function to apply posture damage
    public void ApplyPostureDamage(float damage)
    {
        posture -= damage; // Reduce player's posture by the specified amount

        if (posture <= 0 && !postureBroken)
        {
            postureBroken = true; // Prevent multiple calls to PostureBreak
            anim.SetTrigger("isPosture");
            
        }
    }
}
