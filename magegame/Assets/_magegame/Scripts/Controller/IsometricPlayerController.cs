using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform cameraTarget;

    [SerializeField]
    private Animator anim;

    private Plane groundPlane;

    private Rigidbody rb;

    [SerializeField]
    private float attackRate;

    private float nextAttack;

    private bool isAttacking;
    private bool isMoving;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private Transform spellSpawn;

    [SerializeField]
    private float spellSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
        isMoving = false;
        isAttacking = false;
        nextAttack = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        float dt = Time.deltaTime;
        Move(dt);
        Fire();
        Animate();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("IsHit");
        }

    }

    private void Animate()
    {
        anim.SetBool("IsAttacking", isAttacking);
        anim.SetBool("IsMoving", isMoving);
    }

    private void Move(float dt)
    {
        rb.velocity = Vector3.zero;

        // get axes relative to camera along the ground plane
        Vector3 horizontalAxis = Camera.main.transform.right;
        Vector3 verticalAxis = Vector3.Normalize(Vector3.ProjectOnPlane(Camera.main.transform.forward, groundPlane.normal));

        // calculate final movement vector
        Vector3 horizontalAxisMovement = horizontalAxis * Input.GetAxis("Horizontal");
        Vector3 verticalAxisMovement = verticalAxis * Input.GetAxis("Vertical");
        Vector3 movementVec = Vector3.Normalize(horizontalAxisMovement + verticalAxisMovement);

        // incorporate movement speed and frame time
        movementVec *= moveSpeed * dt;

        // allows us to set the position and bypass acceleration while keeping default collision response
        rb.MovePosition(transform.position + movementVec);

        // debug draw the movement axes
        //Debug.DrawRay(transform.position, horizontalAxis, Color.magenta);
        //Debug.DrawRay(transform.position, -horizontalAxis, Color.yellow);

        //Debug.DrawRay(transform.position, verticalAxis, Color.red);
        //Debug.DrawRay(transform.position, -verticalAxis, Color.blue);

        // follow player with camera
        cameraTarget.position = transform.position;


        // check if moving and set animation
        isMoving = false;

        if(movementVec.magnitude > 0)
        {
            isMoving = true;
        }
    }

    private void Fire()
    {
        if (isAttacking && Time.time > nextAttack)
        {
            isAttacking = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);

            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);

            // draw debug crosshair
            Vector3 line1start = target;
            line1start.x -= 0.5f;

            Vector3 line1end = target;
            line1end.x += 0.5f;

            Vector3 line2start = target;
            line2start.z -= 0.5f;

            Vector3 line2end = target;
            line2end.z += 0.5f;
            Debug.DrawLine(line1start, line1end, Color.red);
            Debug.DrawLine(line2start, line2end, Color.red);

            if (Input.GetButtonDown("Fire1")  && Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                isAttacking = true;
                Attack();
            }
        }
    }

    private void Attack()
    {
        // Create the Spell from the Spell Prefab
        GameObject spell = (GameObject)Instantiate(
            spellPrefab,
            spellSpawn.position,
            spellSpawn.rotation);
        spell.layer = LayerMask.NameToLayer("Player");

        // Add velocity to the Spell
        spell.GetComponentInChildren<Rigidbody>().velocity = spell.transform.forward * spellSpeed;

        // Destroy the Spell after the specified amount of time
        Destroy(spell, 10.0f);
    }
}
