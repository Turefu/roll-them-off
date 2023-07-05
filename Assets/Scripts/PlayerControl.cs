using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    private Vector3 forwardForce;
    public bool hasPoweredUp;
    private float powerupStrength = 15f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        playerRb.AddForce(GetForwardForce());
        powerupIndicator.transform.position = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            hasPoweredUp = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPoweredUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPoweredUp == true)
        {
            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set to" + hasPoweredUp);
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            //需要一个远离玩家的方向，让对手弹飞，用敌人位置-玩家位置来获取
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    private Vector3 GetForwardForce()
    {
        float verticalInput = GetVerticalInput();
        return forwardForce = focalPoint.transform.forward * verticalInput * speed * Time.deltaTime;
    } 


    private float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }
}
