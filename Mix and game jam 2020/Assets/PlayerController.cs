using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    float gridDistance = 0.356666f;
    Vector3 isoXAxis;
    Vector3 isoYAxis;
    public Transform movePoint;

    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;

    [HideInInspector]
    public bool isCollidingRightTop = false;
    [HideInInspector]
    public bool isCollidingRightBottom = false;
    [HideInInspector]
    public bool isCollidingLeftTop = false;
    [HideInInspector]
    public bool isCollidingLeftBottom = false;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movePoint.parent = null;
    }

    private void FixedUpdate()
    {
        isoXAxis = new Vector3(60.0f, 30.0f, 0.0f);
        isoYAxis = new Vector3(-60.0f, 30.0f, 0.0f);
        isoXAxis = isoXAxis.normalized;
        isoYAxis = isoYAxis.normalized;

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Input.GetAxisRaw("Horizontal") == 1f && !isCollidingRightBottom) //Right Bottom
            {
                movePoint.position += (isoYAxis * -gridDistance);
                spriteRenderer.flipX = true;
            }
            if (Input.GetAxisRaw("Vertical") == 1f && !isCollidingRightTop) //Right Top
            {
                movePoint.position += (isoXAxis * gridDistance);
                spriteRenderer.flipX = true;
            }
            if (Input.GetAxisRaw("Horizontal") == -1f && !isCollidingLeftTop) //Left Top
            {
                movePoint.position += (isoYAxis * gridDistance);
                spriteRenderer.flipX = false;
            }
            if (Input.GetAxisRaw("Vertical") == -1f && !isCollidingLeftBottom) //Left Bottom
            {
                movePoint.position += (isoXAxis * -gridDistance);
                spriteRenderer.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        if (otherCollision.CompareTag("Pickup"))
        {
            GameManager.Instance.OnPickupCollected();
            Destroy(otherCollision.gameObject);
            DebugPrint("Picked up an object.");
        }

        if (otherCollision.CompareTag("Enemy"))
        {
            GameManager.Instance.OnHitDeadly();
            DebugPrint("Hit Spikes!");
        }
    }

    /// <summary>
    /// will set the position of the player to the nearest tile
    /// </summary>
    /// <param name="position">position</param>
    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    void DebugPrint(string debugMessage)
    {
        GameObject.FindGameObjectWithTag("DebugObject").GetComponent<Text>().text = "Debug: " + debugMessage;
        StartCoroutine(ResetDebugPrint());
    }

    IEnumerator ResetDebugPrint()
    {
        yield return new WaitForSeconds(3.0f);
        DebugPrint("");
    }
}
