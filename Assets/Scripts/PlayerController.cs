using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private AudioSource cameraaudio;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float sped = 10f;

    [SerializeField] private Vector2 bounds = new Vector2(8,7);

    private int vidas = 3;
    private int score = 0;

    [SerializeField] private ParticleSystem particles;

    [SerializeField] private AudioClip good;
    [SerializeField] private AudioClip bad;

    public bool stop = false;

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(horizontalInput * sped * Time.deltaTime * Vector3.right);
            transform.Translate(verticalInput * sped * Time.deltaTime * Vector3.forward);

            playerinbounds();
        }
    }

    private void playerinbounds()
    {
        Vector3 pos = transform.position;

        if (pos.x < -bounds.x)
        {
            pos.x = -bounds.x;
        }
        else if (pos.x > bounds.x)
        {
            pos.x = bounds.x;
        }
        if (pos.z > bounds.y)
        {
            pos.z = bounds.y;
        }
        else if (pos.z < -bounds.y)
        {
            pos.z = -bounds.y;
        }
        
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Good Coin") && !stop)
        {
            Destroy(collision.gameObject);
            score += 5;
            Debug.Log(score/5);
            Instantiate(particles, transform.position, Quaternion.identity);
            cameraaudio.PlayOneShot(good);
            if (score >= 50) 
            {
                stop = true;
            }
        }
        else if (collision.gameObject.CompareTag("Bad Coin") && !stop)
        {
            Destroy(collision.gameObject);
            vidas--;
            Instantiate(particles, transform.position, Quaternion.identity);
            cameraaudio.PlayOneShot(bad);
            animator.SetTrigger("coll");
            if (vidas == 0)
            {
                stop = true;
            }
        }
    }
}
