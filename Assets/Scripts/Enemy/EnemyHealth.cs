using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100; // health awal
    public int currentHealth; // health sekarang
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10; // skor yang didapatkan jika enemy mati
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        // menapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // set current health
        currentHealth = startingHealth;
    }


    void Update()
    {
        // check jika sinking
        if (isSinking)
        {
            // memindahkan object kebawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // check jika dead
        if (isDead)
            return;

        // play audio
        enemyAudio.Play();

        // kurangi health
        currentHealth -= amount;

        // ganti posisi particle
        hitParticles.transform.position = hitPoint;

        // play particle system
        hitParticles.Play();

        // dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        // set isdead
        isDead = true;

        // setCapcollider ke trigger
        capsuleCollider.isTrigger = true;

        // trigger play animation Dead
        anim.SetTrigger("Dead");

        // play Sound Dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        // disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // set rigisbody ke kimematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
