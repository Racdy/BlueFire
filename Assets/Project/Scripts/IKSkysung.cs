using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSkysung : MonoBehaviour
{

    public Animator skysungAnimator;
    public AnimatorStateInfo skysungRAInfo;
    public AnimatorStateInfo skysungDCInfo;
    public WeaponController IK;

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
        skysungRAInfo = skysungAnimator.GetCurrentAnimatorStateInfo(3);
        skysungDCInfo = skysungAnimator.GetCurrentAnimatorStateInfo(4);

        if (IK.enableIK)
        {

            Ray lockAtRay = new Ray(transform.position, Camera.main.transform.forward);
            Debug.DrawRay(transform.position, Camera.main.transform.forward);

            skysungAnimator.SetLookAtPosition(lockAtRay.GetPoint(50));
            skysungAnimator.SetLookAtWeight(1f, 1f, 1f, 1f, 0.5f);

            //skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(-0.71f, -19.8f, 1.93f));

            if (skysungRAInfo.IsName("AimingRifleA34"))
                skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(-1.72f, -10, 1.42f));
            else if(skysungRAInfo.IsName("AimingShotRifleA34"))
                skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(0f, 0f, 1.42f));
            else if (skysungDCInfo.IsName("AimingDC"))
                skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(13.84f,-20.33f,22.4f));
            else if (skysungDCInfo.IsName("DCShootAIM"))
                skysungAnimator.SetBoneLocalRotation(HumanBodyBones.RightHand, Quaternion.Euler(13.84f, -20.33f, 22.4f));
            
        }

    }
}
