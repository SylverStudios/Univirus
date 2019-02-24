using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float consumptionFactor;
    public Text countText;
    public Text resultText;
    public Text infoText;

    private Rigidbody2D rb2d;
    private CircleCollider2D circleCollider2D;
    private int count;

    void Start() {
        Time.timeScale = 1.0F;
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        count = 0;
        resultText.text = "";
        infoText.text = "";
        SetCountText();
    }

    void Update() {
        if (resultText.text != "")
        {
            if (Input.GetKey(KeyCode.Space)) { SceneManager.LoadScene("Main"); }
        }

        if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PickUp"))
        {
            if (ScaledRadius(other.gameObject) >= ScaledPlayerRadius())
            {
                Time.timeScale = 0;
                resultText.text = "You lose!";
                SetInfoTextForRestart();
            }
            else
            {
                float newRadiusFactor = CalculateNewRadiusFactor(other.gameObject);
                transform.localScale = new Vector3(
                    transform.localScale.x * newRadiusFactor,
                    transform.localScale.y * newRadiusFactor,
                    transform.localScale.z
                );
                Destroy(other.gameObject);
                count = count + 1;
                SetCountText();
            }
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            resultText.text = "You win!";
            SetInfoTextForRestart();
        }
    }

    void SetInfoTextForRestart() {
        infoText.text = "Press Space to restart.";
    }

    public float ScaledPlayerRadius() {
        return circleCollider2D.radius * transform.localScale.x;
    }

    private float ScaledRadius(GameObject other) {
        return other.GetComponent<CircleCollider2D>().radius * other.transform.localScale.x;
    }

    private float CalculateNewRadiusFactor(GameObject other) {
        return Mathf.Sqrt(
            ScaledPlayerRadius() * ScaledPlayerRadius() + consumptionFactor * ScaledRadius(other) * ScaledRadius(other)
        ) / ScaledPlayerRadius();
    }
}
