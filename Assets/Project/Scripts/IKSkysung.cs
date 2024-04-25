using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSkysung : MonoBehaviour
{

    public Animator skysungAnimator;
    public AnimatorStateInfo skysungStateInfo;

    /*public float IKWeight;
    public float IKBody;
    public float IKHead;
    public float IKEyes;
    public float IKClamp;*/

    public float IKX;
    public float IKY;
    public float IKZ;

    // Start is called before the first frame update
    void Start()
    {
        skysungAnimator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        skysungStateInfo = skysungAnimator.GetCurrentAnimatorStateInfo(3);

        if (skysungStateInfo.IsName("AimingRifleA34") || skysungStateInfo.IsName("AimingShotRifleA34"))
        {

            Ray lockAtRay = new Ray(transform.position, Camera.main.transform.forward);
            Debug.DrawRay(transform.position, Camera.main.transform.forward);

            skysungAnimator.SetLookAtPosition(lockAtRay.GetPoint(50));
            skysungAnimator.SetLookAtWeight(1f, 1f, 1f, 1f, 0.5f);

            skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(-0.71f, -19.8f, 1.93f));
            if (skysungStateInfo.IsName("AimingRifleA34")){
                skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(-0.71f, -19.8f, 1.93f));
            }
            else if(skysungStateInfo.IsName("AimingShotRifleA34"))
            {
                skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(0f, -9F, 0f));
            }
        }

    }
}
