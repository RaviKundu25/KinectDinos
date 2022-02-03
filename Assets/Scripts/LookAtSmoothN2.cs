using System;
using System.Collections;
using UnityEngine;

public class LookAtSmoothN2 : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    public GameObject creature;
    public Transform initialTransform;
    public GameObject leftSideEdge;
    public GameObject rightSideEdge;

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
    float targetCreatureMinDist;
    [SerializeField]
    float playerCreatureMaxDist;
    [SerializeField]
    bool targetReached;
    [SerializeField]
    bool playerTooFar;

    private Coroutine LookCoroutine;

    [SerializeField]
    bool facingSameSide = false;

    private void Start()
    {
        target = player.transform;
    }

    private void Update()
    {
        if (playerCreatureMaxDist <= Vector3.Distance(player.transform.position, creature.transform.position))
        {
            playerTooFar = true;
            if (target.name == player.name)
            {
                target = leftSideEdge.transform;
            }
        }
        else
        {
            playerTooFar = false;
            target = player.transform;
        }

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
        }

        if (rotating)
        {
            creature.transform.Rotate(0, 5, 0);
            creature.transform.GetChild(0).GetComponent<Animator>().Play("Carn|Walk");
        }

        if (!rotating && !walking && !targetReached)
        {
            creature.transform.GetChild(0).GetComponent<Animator>().Play("Carn|Walk");
            walking = true;
        }

        if (walking)
        {
            creature.transform.GetChild(0).GetComponent<Animator>().Play("Carn|Walk");
            float step = walkingSpeed * Time.deltaTime;
            creature.transform.position = Vector3.MoveTowards(creature.transform.position, target.transform.position, step);
        }

        if (targetCreatureMinDist >= Vector3.Distance(target.transform.position, creature.transform.position))
        {
            if (!playerTooFar)
            {
                targetReached = true;
                walking = false;
                creature.transform.GetChild(0).GetComponent<Animator>().Play("Carn|Idle1A");
            }
            else
            {
                if(target.name == leftSideEdge.name)
                {
                    target = rightSideEdge.transform;
                }
                else
                {
                    target = leftSideEdge.transform;
                }
            }

        }
        else
        {
            targetReached = false;
        }
    }
}