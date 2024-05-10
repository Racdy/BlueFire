using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIK : MonoBehaviour
{
    public Animator droneAnimator;
    private Transform playerTransform;
    private DroneBehaviour droneBehaviour;

    public float IKWeight;
    public float IKBody;
    public float IKHead;
    public float IKEyes;
    public float IKClamp;

    // Start is called before the first frame update
    void Start()
    {
        droneAnimator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        droneBehaviour = GetComponent<DroneBehaviour>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (droneBehaviour.enableIK)
        {
            Ray lockAtRay = new Ray(transform.position, Camera.main.transform.forward);
            droneAnimator.SetLookAtPosition(playerTransform.position + new Vector3(0.0f, 1f, 0.0f));
            droneAnimator.SetLookAtWeight(0.8f, 1f, 1f, 1f, 0.5f);
        }
    }
}
