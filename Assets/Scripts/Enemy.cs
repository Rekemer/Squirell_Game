using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyHealth))]
public abstract class Enemy : MonoBehaviour
{
   protected Light spotLight;
   public Transform playerPos { get; private set; }
   protected Vector3 WatchDir;
   protected float viewAngle;
    
    public float viewDistance;

    public float defaultSpeed = 2f;
    [SerializeField]
    [Range(1f, 10f)]
    protected float timeOfNotSeeing = 1f;
    protected Vector3 prevPos;

    public Vector3 currVel{ get;  protected set; }

    protected IEnumerator currentCoroutine;
    public bool isCollided = false;
    GameUI UI;

    public enum State
    {
        Attacking, Idle,JustShooting
    }

    public  State state;

    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

    public Transform pathHolder;

    [SerializeField]
    [Range(0f, 10f)]
    protected float howCloseToTurn;

  
    [SerializeField]
    private LayerMask layerMask;

    public float TimeWait = 1f;
    Projectile_Launcher launcherProj;
    private bool haveLauncher;
    protected virtual void Awake()
    {
        UI = FindObjectOfType<GameUI>();
        playerPos = FindObjectOfType<Player>().transform;
        launcherProj = GetComponent<Projectile_Launcher>();
        if (launcherProj != null)
        {
            haveLauncher = true;
        }
        viewAngle = gameObject.transform.GetChild(0).gameObject.GetComponent<Light>().spotAngle;//FindObjectOfType<spotLight>.spotAngle;
        //print("View_Angle" + viewAngle+ " Of gameObject"+ gameObject.name);
    }
    private void Start()
    {

        if (state != State.JustShooting) state = State.Idle;
        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }

        if (state != State.JustShooting)
        {
            currentCoroutine = FollowPath(globalWaypoints);
            StartCoroutine(currentCoroutine);
        }
        
        if (GetComponent<Controller_2D>().enabled == false)
        {
            StartCoroutine(CalcVelocity());
        }
        
    }
    protected virtual void Update()
    {
       
        if (GameUI.gameOver)
        {
           gameObject.GetComponent<Enemy>().enabled = false;
            state = State.Idle;
        }
        else if (haveLauncher && launcherProj.justShoot)
        {
            state = State.JustShooting;
        }
        else if (CanSeePlayer() && !GameUI.gameOver)
        {
            state = State.Attacking;
            
        }
       
        
    }
    public bool CanSeePlayer()
    {
        if (GameUI.gameOver == true || playerPos == null)
        {
            return false;
        }
        if ((transform.position - playerPos.position).sqrMagnitude < Mathf.Pow(viewDistance,2))
        {
            if ((transform.position - playerPos.position).sqrMagnitude < Mathf.Pow(howCloseToTurn, 2) && state == State.Idle && 
                !Physics2D.Linecast(transform.position, playerPos.position, layerMask)){
                TurnTo(playerPos.position);
               
            }
            Vector2 dirToPlayer = (playerPos.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.right, dirToPlayer);
            
            if (angle < viewAngle / 2f)
            {
                
                if (!Physics2D.Linecast(transform.position, playerPos.position, layerMask)) 
                {
                   
                   
                   //print(true);
                    return true;
                }
               
            }
          
        }
      
        return false;
    }
    
    IEnumerator FollowPath(Vector3[] waypoints)
    {
       if (waypoints.Length == 0)
        {
            while (true)
            {
            if (state != State.Idle)
            {
                yield return Attack();
                
            }
            yield return null;

            }
        }
        transform.position = waypoints[0];

        int targetWayPointIndex = 1;
       
        Vector3 targetWayPoint = waypoints[0];
       
        TurnTo(targetWayPoint);
        while (true)
        {

            if (state != State.Idle)
            {
                yield return Attack();
                TurnTo(targetWayPoint);

            }
            
            targetWayPoint.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, defaultSpeed * Time.deltaTime);
            if (transform.position.x == targetWayPoint.x)
            {

                targetWayPointIndex = (targetWayPointIndex + 1) % waypoints.Length;
                targetWayPoint = waypoints[targetWayPointIndex];

                yield return new WaitForSeconds(TimeWait); // to wait after reaching point

                TurnTo(targetWayPoint);
                
            }
            yield return null;
        }


    }
   
    protected abstract IEnumerator Attack();

    protected void TurnTo(Vector3 whereToTurn)
    {
       
        Vector2 dirV = (whereToTurn - transform.position).normalized; // direction from transform position to where to turn
        WatchDir = dirV;
        float degrees = Mathf.Atan2(dirV.y, dirV.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(degrees) > 90)
        {
            degrees = 180;
        }
        else degrees = 0;


        transform.eulerAngles = new Vector3(0, 1 * degrees, 0);
        return;
    }
    void OnDrawGizmos()
    {
        
        //Gizmos.DrawLine(transform.position, playerPos.position);
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }

        }
    }
    protected IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            // Position at frame start
            prevPos = transform.position;
            // Wait till it the end of the frame
            yield return new WaitForEndOfFrame();
            // Calculate velocity: Velocity = DeltaPosition / DeltaTime
            currVel = (prevPos - transform.position) / Time.deltaTime;
            
            //Debug.Log( currVel );
        }
    }

}
