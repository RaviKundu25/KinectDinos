using System;
using System.Collections;
using UnityEngine;

public class LookAtSmooth : MonoBehaviour
{
    public GameObject target;
    public GameObject creature;
    [SerializeField]
    float rotationSpeed = 1f;
    [SerializeField]
    bool rotating = false;
    [SerializeField]
    float walkingSpeed = 1f;
    [SerializeField]
    bool walking = false;
    [SerializeField]
    float timer = 0;
    [SerializeField]
    float playerCreatureMinDist;
    [SerializeField]
    bool playerReached;

    private Coroutine LookCoroutine;

    [SerializeField]
    bool facingSameSide = false;

    private void Update()
    {
        Debug.Log((int)Mathf.Abs(creature.transform.localRotation.eulerAngles.y - 180));

        if ((Vector3.Angle(target.transform.forward, creature.transform.forward) == 180 && creature.transform.localPosition.z < target.transform.localPosition.z)
            || (Vector3.Angle(target.transform.forward, creature.transform.forward) == 0 && creature.transform.localPosition.z > target.transform.localPosition.z))
        {
            facingSameSide = false;
        }

        if ((Vector3.Angle(target.transform.forward, creature.transform.forward) == 180 && creature.transform.localPosition.z > target.transform.localPosition.z)
         || (Vector3.Angle(target.transform.forward, creature.transform.forward) == 0 && creature.transform.localPosition.z < target.transform.localPosition.z))
        {
            facingSameSide = true;
            rotating = false;
        }

        if (!facingSameSide && !rotating)
        {
            walking = false;
            rotating = true;
            if ((int)Mathf.Abs(creature.transform.localRotation.eulerAngles.y - 180) <= 1)
            {
                creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canTurnRight", true);
            }

            if ((int)Mathf.Abs(creature.transform.localRotation.eulerAngles.y - 180) >= 179)
            {
                creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canTurnLeft", true);
            }
            creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canWalk", false);
            StartRotating();
        }

        if (!rotating && !walking && !playerReached)
        {
            creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canWalk", true);
            creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canTurnLeft", false);
            creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canTurnRight", false);
            walking = true;
        }

        if (walking)
        {
            float step = walkingSpeed * Time.deltaTime;
            creature.transform.position = Vector3.MoveTowards(creature.transform.position, target.transform.position, step);
        }

        if (playerCreatureMinDist >= Vector3.Distance(target.transform.position, creature.transform.position))
        {
            playerReached = true;
            walking = false;
            creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canWalk", false);
            creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canTurnLeft", false);
            creature.transform.GetChild(0).GetComponent<Animator>().SetBool("canTurnRight", false);
        }
        else
        {
            playerReached = false;
        }
    }

    public void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - creature.transform.position);
        float time = 0;
        Debug.Log("Rotating started");
        while (time < 1)
        {
            creature.transform.rotation = Quaternion.Slerp(creature.transform.rotation, lookRotation, time);
            time += Time.deltaTime * rotationSpeed;
            Debug.Log("Rotating.... :: Time : " + time.ToString());
            if (time > 0.1)
            {
                if ((int)Mathf.Abs(creature.transform.localRotation.eulerAngles.y - 180) == 0 || (int)Mathf.Abs(creature.transform.localRotation.eulerAngles.y - 180) == 180)
                {
                    rotating = false;
                }
            }

            yield return null;
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "LookAt"))
            StartRotating();
    }
}