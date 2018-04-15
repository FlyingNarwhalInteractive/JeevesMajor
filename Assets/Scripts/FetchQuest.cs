using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FetchQuest : MonoBehaviour
{


    // [SerializeField] float fetchDist;
    [SerializeField] int fetchReward;
    [SerializeField] int fetchRage;
    [SerializeField] float wait;
    [SerializeField] GameObject fire;
    //  public bool fetchStarted;
    // public List<GameObject> fetches;
    [SerializeField] float fcount;
    private GameDataStore dataRef;
    private GameObject baronRef;
    private Jeeves jeevesRef;

    NavManager navManager;
    NavMeshPath linePath;
    NavMeshPath linePath2;
    Vector3[] newPath;
    LineRenderer lineRenderer;
    public GameObject lineEndArrow;
    public float arrowAngle = 0;
    public int arrowPos = 1;
    public float lastTimeMoved = 0;
    public float arrowMoveSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {

        dataRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameDataStore>();
        baronRef = GameObject.FindGameObjectWithTag("Baron");
        jeevesRef = GameObject.FindGameObjectWithTag("Jeeves").GetComponent<Jeeves>();
        jeevesRef.isFetchActive = true;
        navManager = FindObjectOfType<NavManager>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jeeves")
        {

            other.gameObject.GetComponent<Jeeves>().hasFetch = true;
            other.gameObject.GetComponent<Jeeves>().fetchReward = fetchReward;
            other.gameObject.GetComponent<Jeeves>().fetchRage = fetchRage;
            other.gameObject.GetComponent<Jeeves>().jeevesFetchTimeout = wait;
            other.gameObject.GetComponent<Jeeves>().line.enabled = true;

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Reference (dotted line)
        // https://answers.unity.com/questions/733592/dotted-line-with-line-renderer.html
        //Answer by kingcoyote · Apr 17, 2016 at 12:46 AM

        // create new line
        linePath = navManager.GetPath(jeevesRef.transform.position, gameObject.transform.position);
        linePath2 = navManager.GetPath(gameObject.transform.position, baronRef.transform.position);

        newPath = new Vector3[linePath.corners.Length + linePath2.corners.Length];
        for (int i = 0; i < linePath.corners.Length; i++)
        {
            newPath[i] = linePath.corners[i];
            newPath[i].y = 0.1f;
        }
        for (int i = 0; i < linePath2.corners.Length; i++)
        {
            newPath[i + linePath.corners.Length] = linePath2.corners[i];
            newPath[i + linePath.corners.Length].y = 0.1f;
        }

        lineRenderer.positionCount = newPath.Length;
        lineRenderer.SetPositions(newPath);

        // arrow presently not working nicely
        /*if (arrowPos >= newPath.Length)
            arrowPos = 1;

        lineEndArrow.transform.position = newPath[arrowPos];
        arrowAngle = Vector2.SignedAngle(newPath[arrowPos - 1], newPath[arrowPos]);
        lineEndArrow.transform.rotation = Quaternion.Euler(90, 0, arrowAngle * 360.0f);

        if (Time.time - lastTimeMoved > arrowMoveSpeed)
        {
            lastTimeMoved = Time.time;
            arrowPos++;
        }*/
        //lineRenderer.SetPosition(0, gameObject.transform.position);
        //lineRenderer.SetPosition(1, baronRef.transform.position);






        fcount += Time.deltaTime;
        if (fcount > wait)
        {

            baronRef.GetComponent<NotTheRealAI>().isPaused = false;
            //punish player
            dataRef.SetCurrentRage(fetchRage);
            fcount = 0;
            jeevesRef.isFetchActive = false;
            if (fire != null)
            {
                GameObject F = Instantiate(fire, transform.position, fire.transform.rotation);
                Destroy(F, 5.0f);
            }
            Destroy(gameObject);


        }
    }
}



