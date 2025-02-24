using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float secondsPerTurn = 0.5f;
    private float lastTurnTime = 0;
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    private float horizonInput;
    private Rigidbody rb;
    private Animator animator;
    public PathGenerator pathGenerator = new PathGenerator();
    private Lanes lanes;
    Segment currentSegment = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    private void Start()
    {

        targetRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        lanes = Lanes.Middle;

        //segmentPositions = pathGenerator.segmentPosition();
    }
    private void Update()
    {

        if (!GameManager.Instance.isGameStarting) return;
        horizonInput = Input.GetAxisRaw("Horizontal");
        Vector3 direction = Vector3.forward;
        direction.x = 0;
        transform.Translate(direction * speed *Time.deltaTime);
        ChangLane();

        if (currentSegment != null && currentSegment.isTurning == true)
        {
            CharacterTurning();
        }
    }
 

    private void ChangLane()
    {
      if(horizonInput > 0)
        {
            transform.Translate(Vector3.right *speed* Time.deltaTime);
        }
      else if (horizonInput < 0)
        {
            transform.Translate(Vector3.left*speed * Time.deltaTime);   
        }

    }
    private void CharacterTurning()
    {
       

        if (Time.time - lastTurnTime < secondsPerTurn)
        {
            float maxDegreeDelta = 90f / secondsPerTurn * Time.deltaTime*5;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreeDelta);
            return;
        }

        if (horizonInput > 0)
        {
            StartTurn(90);

        }
        if (horizonInput < 0)
        {
            StartTurn(-90);

        }
        if (horizonInput == 0) return;


    }
    private void StartTurn(float angle)
    {
        targetRotation *= Quaternion.Euler(0,angle, 0);
        lastTurnTime = Time.time;
    }
  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            GameManager.Instance.isGameOver = true;

        }
        Segment segment = collision.gameObject.GetComponent<Segment>();
        if (segment != null)
        {
            this.currentSegment = segment;
        }

       
    }
   

}
public enum Lanes
{
    Left,
    Right,
    Middle,
}
