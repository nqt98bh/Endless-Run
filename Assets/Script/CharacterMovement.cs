using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement Instance;
    [SerializeField] private float speed;
    [SerializeField] private float defaultSpeed = 10f;
    [SerializeField] private float secondsPerTurn = 0.5f;
    float timeSpeedUpInterval = 10f;
    float timer = 0f;
    private float lastTurnTime = 0;
    private Quaternion targetRotation;
    private float horizonInput;
    Segment currentSegment = null;
    Animator animator;
    bool isTurning = false;
    private bool isSpeedUp = false;

    private void Awake()
    {
       
            Instance = this;
      
    }
    private void Start()
    {
        speed = defaultSpeed;
        targetRotation = transform.rotation;
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        if (GameManager.Instance.IsPausedGame()) return;

        if (!GameManager.Instance.isGameStarting) return;
        if (GameManager.Instance.isGameOver) return;
        horizonInput = Input.GetAxisRaw("Horizontal");
        
        MoveFoward();
        ChangLane();

        if (currentSegment != null && currentSegment.segmentTurn == true  )
        {
            
            CharacterTurning();

        }
        else if (currentSegment.segmentTurn == false)
        {
            isTurning = false;

        }
        if(isSpeedUp == true)
        {
            timer += Time.deltaTime;
            if(timer > timeSpeedUpInterval)
            {
                isSpeedUp = false;
                speed = defaultSpeed;
                timer = 0;
            }
        }
    }

    void MoveFoward()
    {
        Vector3 direction = Vector3.forward;
        direction.x = 0;
        transform.Translate(direction * speed * Time.deltaTime);
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

        if (horizonInput > 0 && isTurning == false)
        {
            StartTurn(90);
            isTurning = true;

        }
        if (horizonInput < 0 && isTurning == false)
        {
            StartTurn(-90);
            isTurning = true;

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
       
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("DeathZone"))
        {
            
            animator.SetTrigger("CollideObstacle");
            GameManager.Instance.isGameOver = true;
            SoundFXManager.Instance.PlaySoundFX(SoundType.Death);


        }
        Segment segment = collision.gameObject.GetComponent<Segment>();
        if (segment != null)
        {
            currentSegment = segment;
        }

       
    }
   
    public void SpeedUp(float amount)
    {
        speed += amount;
        isSpeedUp = true;
    }

}

