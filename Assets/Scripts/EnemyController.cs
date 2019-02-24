using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float startingSpeed;
    public float redirectSpeed;
    public float massModifier;

    private Rigidbody2D rb2d;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Random.onUnitSphere * startingSpeed;

        InvokeRepeating("Redirect", 1.0F, 1.0F);
    }

	void Redirect () {
        StartCoroutine(AddForceOverTime(0.5F));
	}

    IEnumerator AddForceOverTime(float time) {
        float i = time;
        while (i > 0)
        {
            rb2d.AddForce(Random.onUnitSphere * (massModifier * rb2d.mass) * redirectSpeed);
            i += Time.deltaTime;
            yield return null;
        }
    }
}
