using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   
    private CharacterController controller;
    private CapsuleCollider col;
    private Score score;
    private Vector3 dir;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private int coins;
    [SerializeField] private Text coinsText;
    [SerializeField] private Score scoreScript;
    public AudioClip coinSound;

    private int lineToMove = 1;
    public float lineDistanse = 4;
    private float maxSpeed = 100;

    private bool roll;
    private Animator anim;
    private bool isImmortal;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        col = GetComponent<CapsuleCollider>();
        score = scoreText.GetComponent<Score>();
        score.scoreMultiplier = 1;
        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("coins");
        coinsText.text = coins.ToString();
        StartCoroutine(SpeedIncrease());
        isImmortal = false;
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }
        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if(SwipeController.swipeUp)
        {
            if(controller.isGrounded)
            Jump();
        }
         if (SwipeController.swipeDown)
        {
            StartCoroutine(Roll());
        }
        if (controller.isGrounded && !roll)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistanse;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistanse;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void Jump()
    {
        dir.y = jumpForce;
        anim.SetTrigger("Jump");
    }

    
    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            if (isImmortal)
                Destroy(hit.gameObject);
            else
            {
                losePanel.SetActive(true);
                int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
                PlayerPrefs.SetInt("lastRunScore", lastRunScore);
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("coins", coins);
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "BonusStar")
        {
            StartCoroutine(StarBonus());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BonusShield")
        {
            StartCoroutine(BonusShield());
            Destroy(other.gameObject);
        }

        AudioSource.PlayClipAtPoint(coinSound, transform.position); 
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(5);
        if (speed < maxSpeed)
          {
            speed +=3;
            StartCoroutine(SpeedIncrease());
          }
    }

    private IEnumerator Roll()
    {
        col.center = new Vector3(0, 0.4000626f, 0);
        col.height = 0.8290902f;
        roll = true;
        anim.SetTrigger("Roll");
        yield return new WaitForSeconds(2);

        col.center = new Vector3(0, 0.9060115f, 0);
        col.height = 1.840988f;
        roll = false;
    }

    private IEnumerator StarBonus()
    {
        score.scoreMultiplier = 2;

        yield return new WaitForSeconds(5);

        score.scoreMultiplier = 1;
    }

    private IEnumerator BonusShield()
    {
        isImmortal = true;

        yield return new WaitForSeconds(5);

        isImmortal = false;
    }
}
