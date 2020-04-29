using UnityEngine;

public class BirdScripts : MonoBehaviour
{
    Rigidbody2D rb2d;
    bool isStop = false;
    bool didFlap = false;
    float bounceForce = 4f;

    public delegate void EmptyFunc();
    EmptyFunc onEndGameCallBack;
    EmptyFunc onScoreCallBack;

    AudioSource audioSource;

    [SerializeField]
    AudioClip flyClip, pingClip, diedClip;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Stop()
    {
        isStop = true;
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    public void Resume()
    {
        isStop = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public void RegisterOnEndGameCallBack(EmptyFunc cb)
    {
        onEndGameCallBack = cb;
    }

    public void RegisterScoreCallBack(EmptyFunc cb)
    {
        onScoreCallBack = cb;
    }

    public void Jump()
    {
        didFlap = true;
        audioSource.PlayOneShot(flyClip);
    }

    void FixedUpdate()
    {
        if (isStop) return;

        if (didFlap)
        {
            didFlap = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);
        }

        if (rb2d.velocity.y > 0)
        {
            float angel = Mathf.Lerp(0, 90, rb2d.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angel);
        }
        else if (rb2d.velocity.y < 0)
        {
            float angel = Mathf.Lerp(0, -90, -rb2d.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angel);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (!isStop)
        {
            Stop();
            onEndGameCallBack();
            audioSource.PlayOneShot(diedClip);
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            onScoreCallBack();
            audioSource.PlayOneShot(pingClip);
        }
    }
}
