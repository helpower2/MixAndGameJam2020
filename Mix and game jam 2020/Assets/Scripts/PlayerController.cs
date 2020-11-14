using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;

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
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        if (movement.x > 0)
            spriteRenderer.flipX = true;
        else if(movement.x < 0)
            spriteRenderer.flipX = false;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        if (otherCollision.gameObject.tag == "Pickup")
        {
            GameManager.score += 1;
            Destroy(otherCollision.gameObject);
            GetComponent<AudioSource>().Play();
            DebugPrint("Picked up an object.");
        }

        if (otherCollision.gameObject.tag == "Enemy")
        {
            Debug.Log("SPIKES!");
            DebugPrint("Hit Spikes!");
        }
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
