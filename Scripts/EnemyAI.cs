using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    private Health health;
    private int difficulty;

    public Transform target;
    public Animator animator;
    public GameObject Enemy;


    private bool m_Grounded;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public float speed = 200f;

    // How close the enemy needs to be to a waypoint before it moves to the next
    // (As the path finder is made up of several waypoints)
    public float nextWaypointDistance = 5f;


    public Transform enemyGFX;
    public Player player;

    Pathfinding.Path path; // current path the enemy is following
    int currentWaypoint = 0; // Current waypoint of the path we are following
    bool reachedEndOfPath = false; // Have we reached end of path?

    Seeker seeker;
    Rigidbody2D rb;

    public int damageToGive;

    // Start is called before the first frame update
    void Start()
    {
        checkDifficulty();
        health = GetComponent<Health>();

        //Difficulty section -- This is opposite of the player
        if (difficulty == -1) // Easy
        {
            health.setMax(health.getMax() / 2);// Half max health
            health.setCurrent(health.getMax());// Sets current health to max health
        }
        else if (difficulty == 0) // Medium
        {
            health.setMax(health.getMax());
            health.setCurrent(health.getMax());// Sets current health to max health
        }
        else
        {
            health.setMax(health.getMax() * 2);// Double max health
            health.setCurrent(health.getMax());// Sets current health to max health
        }

        maxHealth = health.getMax();
        currentHealth = health.getCurrent();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // Calls UpdatePath, every half second to continue updating the path as needed
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Called when a trigger collides with the enemy
    private void OnTriggerEnter2D(Collider2D gameObject)
    {
        // If it is the player
        if (gameObject.name == player.name)
        {
            player.TakeDamage(damageToGive); // Damage the player
            var playerP = gameObject.GetComponent<CharacterController2D>(); // Knockback the player in right direction based on enemy position
            playerP.knockbackCount = playerP.knockbackLength;

            if (playerP.transform.position.x > transform.position.x)
            {
                playerP.hitFromRight = false;
            }
            else
            {
                playerP.hitFromRight = true;
            }

        }
    }

    public void TakeDamage(int damage)
    {
        if (difficulty == -1) //Take double damage -- This is the opposite of the player class
        {
            health.takeDmg(damage * 2);
        }
        else if (difficulty == 0) //Take normal damage
        {
            health.takeDmg(damage);
        }
        else //Take half damage
        {
            health.takeDmg(damage / 2);
        }
        
        //Play hurt animation
        animator.SetTrigger("Hurt");
        if (!health.alive())
        {
            Die();
        }
    }


    void Die()
    {
        // Die animation
        animator.SetBool("IsDead", true);

        // Sets enemy gravity to 0 so it doesn't fall when collider is removed, then destroys enemy object
        rb.gravityScale = 0;
        Destroy(Enemy, 1f);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }

    // UpdatePath() will start a new path from the enemy position to the player position, calling OnPathComplete on path complete.
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // OnPathComplete(Path p) will make sure there were no errors within the path. And generate a new path and reset the waypoint to the start.
    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }


    void FixedUpdate()
    {

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
        // If there is no path, return (Don't move)
        if (path == null)
        {
            return;
        }

        /* 
         * If the current waypoint is greater than or equal to the total amount of waypoints of our path
         * Then enemy has reached end of its path and it will return. (Stop moving) 
        */
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // The direction our enemy wants to move
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // The force our enemy will have given direction, speed, and the time to keep it consistent across all frame rates.
        Vector2 force = direction * speed * Time.deltaTime;

        // Give our enemy its force across its path
        if (m_Grounded && force.y > 7)
        {
            // Add a vertical force to the enemy if needed
            m_Grounded = false;
            rb.AddForce(new Vector2(force.x, 600));
        }
        else
        {
            rb.AddForce(new Vector2(force.x, 0)); // Otherwise give normal force to enemy
        }
        // Distance between enemy position and the next waypoint on the path
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        // If that distance is less than the next waypoint distance, then we have reached the current waypoint and need to move on to the next
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Changes which way the enemy is looking based on the force in any given direction
        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-10f, 10f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(10f, 10f, 1f);
        }

        // Gives walking animation upon movement
        animator.SetFloat("Speed", Mathf.Abs(force.x));
    }

    //Checks the difficulty and sets the difficulty
    private void checkDifficulty()
    {
        if (!File.Exists("diff.txt"))//Creates the diff file if haven't been created.
        {
            File.WriteAllText("diff.txt", difficulty.ToString());
        }
        else
        {
            string inDiff = File.ReadAllText("diff.txt");
            int diff;
            bool succ = int.TryParse(inDiff, out diff);
            if (succ)
            {
                if (diff > 1 || diff < -1) //If Difficulty got set in the text file more or less than what's available.
                {
                    print("Difficulty out of bounds, set to normal");//For log/debugging
                    difficulty = 0; //Sets the difficulty to medium
                    File.WriteAllText("diff.txt", difficulty.ToString());
                    print("Difficulty set to " + difficulty.ToString());//For log/debugging
                }
                else //only if everything has gone right.
                {
                    difficulty = diff;
                }
            }
            else
            {
                File.WriteAllText("diff.txt", difficulty.ToString());
            }
            
        }
    }
}
