using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject player;
    private Animator _animator;
    private float distance;
    private bool parried;
    [SerializeField] GameObject sword;
    [SerializeField] private Material red, normal;

    [SerializeField] private float minDis;
    [SerializeField] private float postureDMG;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        Attacking();
    }

    private void Attacking()
    {
        if (distance < minDis)
        {
            _animator.SetBool("inRange", true);
        }
        else
        {
            _animator.SetBool("inRange", false);
        }
    }

    public void ParryableStart()
    {
        parried = true;
    }

    private void ParryAbleEnd()
    {
        parried = false;
    }

    public bool Parried()
    {
        if (parried)
        {
            _animator.SetTrigger("Parried");
        }

        return parried;
    }

    private void ParrySignal()
    {
        sword.GetComponent<MeshRenderer>().material = red;
    }

    private void ParrySignalStop()
    {
        sword.GetComponent<MeshRenderer>().material = normal;
    }

    // Function to apply posture damage
    public void ApplyPostureDamage(float damage)
    {
        var pCombat = player.gameObject.GetComponent<PCombat>();
        pCombat.posture -= damage; // Use -= to subtract posture damage
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerScript = collision.gameObject.GetComponent<PCombat>();
            if (playerScript.blocking)
            {
                // Apply posture damage to the player when the enemy collides with the player's weapon
                ApplyPostureDamage(postureDMG);
            }
            
        }
    }
}
