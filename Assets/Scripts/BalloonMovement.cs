using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonMovement : MonoBehaviour
{
    public float scaleFactor = 1.2f;
    public float interval = 2.0f; 
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int SPEED = 5;
    [SerializeField] bool isFacingRight = true;

    [SerializeField] public AudioSource audio;

    [SerializeField] public GameObject player;

    [SerializeField] GameObject controller;

     public GameObject distractor;
    private int points = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // if (distractor == null) {
        //     distractor = GameObject.FindGameObjectWithTag("Distractor");
        // }
        //  Physics2D.IgnoreCollision(distractor.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        //  Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Distractor"), true);

        GameObject[] distractors = GameObject.FindGameObjectsWithTag("Distractor");
        foreach (GameObject distractor in distractors)
        {
            Physics2D.IgnoreCollision(distractor.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }

        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("Score");
        }
         InvokeRepeating("getBigger", 1.5f, interval);

        

        

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * SPEED * Time.deltaTime);

        if (!IsObjectVisible())
        {
            // Reverse the direction by changing the sign of the speed
            // SPEED = -SPEED;
            Flip();
        }
       


        Vector3 currentScale = transform.localScale;
         if (currentScale.x >= 4.5f) {
            // Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         }
    }

    void getBigger() {
         Vector3 currentScale = transform.localScale;

        // Increase the size by the scaleFactor
        currentScale *= scaleFactor;

        // Set the new scale of the object
        transform.localScale = currentScale;

        points -= 20;
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

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Arrow")) {
            controller.GetComponent<ScoreManager>().AddPoints(points);
            // controller.GetComponent<ScoreManager>().AddPoints(100);
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            Destroy(gameObject);
            // audio.Play();
        }
    }
}
