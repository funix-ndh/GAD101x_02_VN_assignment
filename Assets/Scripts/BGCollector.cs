using UnityEngine;

class BGCollector : MonoBehaviour
{
    GameObject[] backgrounds;
    GameObject[] grounds;

    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background" || target.tag == "Ground")
        {
            target.gameObject.transform.position = new Vector3(target.gameObject.transform.position.x + 28f, target.gameObject.transform.position.y);
        }
    }

    void Update()
    {
        foreach (GameObject gameObject in backgrounds)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - GameController.instance.GetGameSpeed(), gameObject.transform.position.y);
        }
        foreach (GameObject gameObject in grounds)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - GameController.instance.GetGameSpeed(), gameObject.transform.position.y);
        }
    }
}
