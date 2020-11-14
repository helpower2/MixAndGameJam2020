using System.Collections;
using System.Collections.Generic;
using Game.Beat;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxDistancePerBeat = 1f;
    public Transform movePoint;

    float gridDistance = 0.356666f;
    Vector3 isoXAxis;
    Vector3 isoYAxis;

    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Vector3 spawnPoint;

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
        spawnPoint = transform.position;
        movePoint.parent = null;
    }

    private void FixedUpdate()
    {
        isoXAxis = new Vector3(60.0f, 30.0f, 0.0f);
        isoYAxis = new Vector3(-60.0f, 30.0f, 0.0f);
        isoXAxis = isoXAxis.normalized;
        isoYAxis = isoYAxis.normalized;

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, (maxDistancePerBeat * (Time.fixedDeltaTime / BeatIndex.SecondsPerBeat)));

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (GameManager.dead || GameManager.won) return;
            
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

    private void OnTriggerStay2D(Collider2D otherCollision)
    {
        if (otherCollision.CompareTag("Pickup"))
        {
            Debug.Log(GameManager.Instance.ToString());
            GameManager.Instance.OnPickupCollected();
            Destroy(otherCollision.gameObject);
            DebugPrint("Picked up an object.");
        }

        if (otherCollision.CompareTag("Enemy"))
        {
            GameManager.Instance.OnHitDeadly();
            DebugPrint("Hit Spikes!");
        }

        if (otherCollision.CompareTag("Finish"))
        {
            GameManager.Instance.OnPlayerFinish();
        }

        if (otherCollision.CompareTag("Void"))
        {
            GameManager.Instance.OnHitDeadly();
            DebugPrint("fell into the void");
        }
    }

    /// <summary>
    /// will set the position of the player to the nearest tile
    /// </summary>
    /// <param name="position">position</param>
    public void SetPosition(Vector2 position)
    {
        transform.position = position;
        movePoint.position = position;
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
