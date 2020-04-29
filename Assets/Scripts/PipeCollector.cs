using UnityEngine;

class PipeCollector : MonoBehaviour
{
    [SerializeField]
    GameObject pipeHolder1;

    [SerializeField]
    GameObject pipeHolder2;

    GameObject last;
    readonly GameObject[] pipes = new GameObject[6];

    void Start()
    {
        bool pipeToggle = true;
        for (int i = 0, x = 5; i < 6; i++, x += 5)
        {
            pipes[i] = Instantiate(pipeToggle ? pipeHolder1 : pipeHolder2, new Vector3(x, RandomY()), Quaternion.identity);
            pipeToggle = !pipeToggle;
        }
        last = pipes[5];
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            target.transform.position = new Vector3(last.transform.position.x + 5, RandomY());
            last = target.gameObject;
        }
    }

    float RandomY()
    {
        return Random.Range(-2f, 4f);
    }

    void Update()
    {
        foreach (GameObject gameObject in pipes)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - GameController.instance.GetGameSpeed(), gameObject.transform.position.y);
        }
    }
}
