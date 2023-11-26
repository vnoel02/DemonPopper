using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorPath : MonoBehaviour
{
    [SerializeField] int SPEED = 8;
    [SerializeField] bool isFacingRight = true;
    // Start is called before the first frame update
    [SerializeField] GameObject balloon;
    void Start()
    {
         if (balloon == null) {
            balloon = GameObject.FindGameObjectWithTag("Monster");
        }
        
        Physics2D.IgnoreCollision(balloon.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }

    // Update is called once per frame
    void Update()
    {

         transform.Translate(-Vector2.right * SPEED * Time.deltaTime);
         if (!IsObjectVisible())
        {
            // Reverse the direction by changing the sign of the speed
            
           Flip();
        }
    }

     bool IsObjectVisible()
    {
        // Check if the object is outside the camera's view
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1;
    }

     private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    // private void OnCollisionEnter2D(Collision2D collision) {

    //     if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Arrow")) {
    //         return;
    //     }
    //     Destroy(gameObject);
    // }
}
