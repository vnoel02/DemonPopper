using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPath : MonoBehaviour
{
    [SerializeField] int SPEED = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector2.right * SPEED * Time.deltaTime);
         if (!IsObjectVisible())
        {
            // Reverse the direction by changing the sign of the speed
            
            Destroy(gameObject);
        }
    }

    bool IsObjectVisible()
    {
        // Check if the object is outside the camera's view
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Arrow")) {
            return;
        }
        Destroy(gameObject);
    }
}
