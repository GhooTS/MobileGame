using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HingeJoint))]
public class BallController : MonoBehaviour
{
    public float pullSpeed = 20f;
    public float forceBuildUp = 20f;
    public float catchedRange = 6f;

    [Header("Events")]
    public UnityEvent onBallPulled;
    public UnityEvent onBallcatched;
    public UnityEvent onBallReleasted;


    private IPlayerControls playerControls;

    private BoxCollider catcheCollider;
    private SphereCollider rangeCheckCollider;
    private HingeJoint joint;

    private bool ballAttached = false;
    private Ball ball;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
        playerControls = GetComponent<IPlayerControls>();
        ball = FindObjectOfType<Ball>();

        catcheCollider = gameObject.AddComponent<BoxCollider>();
        catcheCollider.size = new Vector3(.25f,1f,catchedRange);
        
        rangeCheckCollider = gameObject.AddComponent<SphereCollider>();
        rangeCheckCollider.radius = catchedRange / 2f + .3f;
        rangeCheckCollider.enabled = false;

        rangeCheckCollider.isTrigger = catcheCollider.isTrigger = true;
    }


    private void Update()
    {
        if (playerControls.GetFireInput())
        {
            if (ballAttached) ReleasteBall();
            else if(rangeCheckCollider.enabled == false) PullBall();
        }
    }

    private void ReleasteBall()
    {
        FreeBall(true);
        ball.SetArmed(true);
        onBallReleasted?.Invoke();
    }

    private void PullBall()
    {
        ball.Velocity = (transform.position - ball.transform.position).normalized * pullSpeed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (rangeCheckCollider.enabled || !other.CompareTag("Ball")) return;

        Rigidbody catchedBall;
        catchedBall = other.attachedRigidbody;

        if (ballAttached == false)
        {
            joint.connectedBody = catchedBall;
            joint.autoConfigureConnectedAnchor = false;
            ballAttached = true;
            ball.SetArmed(false);
            onBallcatched?.Invoke();
        }

        catchedBall.AddForce(catchedBall.velocity.normalized * forceBuildUp);
    }


    public void FreeBall(bool isInRange)
    {
        joint.connectedBody = null;
        joint.autoConfigureConnectedAnchor = true;
        ballAttached = false;
        catcheCollider.enabled = !isInRange;
        rangeCheckCollider.enabled = isInRange;
    }


    private void OnTriggerExit(Collider other)
    {
        if (rangeCheckCollider.enabled == false || ballAttached || !other.CompareTag("Ball")) return;

        rangeCheckCollider.enabled = false;
        catcheCollider.enabled = true;
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(.25f,1f, catchedRange));
        Gizmos.DrawWireSphere(transform.position, catchedRange / 2f + 0.3f);
    }
}
