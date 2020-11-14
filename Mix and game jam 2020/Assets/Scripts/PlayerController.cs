using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    Vector2 newXPos = Vector2.zero;
    Vector2 newYPos = Vector2.zero;

    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput) * (GameManager.dead? 0:1);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        //Convert input axes to isometric input axes
        Vector2 isoXAxis = new Vector2(60.0f, 30.0f);
        Vector2 isoYAxis = new Vector2(-60.0f, 30.0f);
        isoXAxis = Vector2.ClampMagnitude(isoXAxis, 1.0f);
        isoYAxis = Vector2.ClampMagnitude(isoYAxis, 1.0f);
        Vector2 isoInputVector = isoXAxis + isoYAxis;
        if (horizontalInput != 0)
        {
            Vector2 movement = isoXAxis * horizontalInput * movementSpeed;
            if (movement.x > 0)
                spriteRenderer.flipX = true;
            else if (movement.x < 0)
                spriteRenderer.flipX = false;
            newXPos = movement * Time.fixedDeltaTime;
        }
        if (verticalInput != 0)
        {
            Vector2 movement = isoYAxis * verticalInput * movementSpeed;
            newYPos = movement * Time.fixedDeltaTime;
        }
        rbody.MovePosition(currentPos + newXPos + newYPos);

        //Vector2 movement = inputVector * movementSpeed;
        //if (movement.x > 0)
        //    spriteRenderer.flipX = true;
        //else if (movement.x < 0)
        //    spriteRenderer.flipX = false;
        //Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //rbody.MovePosition(newPos);
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
