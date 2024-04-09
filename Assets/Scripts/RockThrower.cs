using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject rockInHandVisual;
    [SerializeField] private bool infiniteRocks = false;
    [SerializeField] private GameObject pickUpText;
    [SerializeField] private GameObject cameraCenter;
    [SerializeField] private Transform throwPos;
    [SerializeField] private Vector3 throwDirection = new Vector3(0, 1, 0);
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse0;

    [Header("Rock Force")]
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float maxForce = 20f;

    private bool isCharging = false;
    private bool hasRock = false;
    private float chargeTime = 0f;


    private void Awake()
    {
        rockInHandVisual.SetActive(false);
        pickUpText.SetActive(false);
        
    }

    private void OnEnable()
    {
        rockInHandVisual.SetActive(false);
        pickUpText.SetActive(false);
        hasRock = false;
        isCharging = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Rock") && !infiniteRocks && !hasRock) //can only pickup rocks if they dont have one/dont have infinite rocks and are near a rock.
        {
            pickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                rockInHandVisual.SetActive(true);
                hasRock = true;
                other.gameObject.SetActive(false);
                pickUpText.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickUpText.SetActive(false);
    }

    private void Update()
    {
        if (infiniteRocks)
        {
            hasRock = true;
        }
        if (Input.GetKeyDown(throwKey) && hasRock == true) //starts throwing action
        {
            StartThrow();
        }
        if (isCharging)   
        {
            ChargeThrow();
        }
        if (Input.GetKeyUp(throwKey) && hasRock == true) //throws the rock
        {
            ReleaseThrow();
        }
    }

    void StartThrow()
    {
        isCharging = true;
        chargeTime = 0f;
    }

    void ChargeThrow()
    {
        chargeTime += Time.deltaTime; //increases power of the throw
    }

    void ReleaseThrow()
    {
        ThrowRock(Mathf.Min(chargeTime * throwForce, maxForce));
        hasRock = false;
        rockInHandVisual.SetActive(false);

    }

    void ThrowRock(float force)  //creates a new rock and applies all the relavant forces
    {
        Vector3 spawnPos = throwPos.position + cameraCenter.transform.forward;

        GameObject rock = Instantiate(rockPrefab, spawnPos, cameraCenter.transform.rotation);

        Rigidbody rb = rock.GetComponent<Rigidbody>();

        Vector3 finalThrowDirection = (cameraCenter.transform.forward + throwDirection).normalized;
        rb.AddForce(finalThrowDirection * force, ForceMode.Impulse);
    }

}
