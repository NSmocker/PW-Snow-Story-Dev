using UnityEngine;
using UnityEngine.Animations.Rigging;
public class NPCLookToPlayer : MonoBehaviour
{
    public Transform headObject, lookAtPlayerObject;
    public float minDistanceToFocus = 3f;
    public float smoothWeightLerpFactor = 1f;
    
    public MultiAimConstraint headLookConstraint;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headLookConstraint = GetComponent<MultiAimConstraint>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(headObject.position, lookAtPlayerObject.position) < minDistanceToFocus)
        {
            headLookConstraint.weight = Mathf.Lerp(headLookConstraint.weight, 1f, smoothWeightLerpFactor * Time.deltaTime);
        }
        else
        {
            headLookConstraint.weight = Mathf.Lerp(headLookConstraint.weight, 0f, smoothWeightLerpFactor * Time.deltaTime);
        }
    }
}
