using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjAsPerKinect : MonoBehaviour
{
    [SerializeField]
    GameObject objToMoveAlong;
    [SerializeField]
    GameObject objToMove;

    private void Update()
    {
        //objToMove.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.WorldToScreenPoint(objToMoveAlong.transform.position).x,0,0));

        objToMove.transform.localPosition = new Vector3(0, 0, objToMoveAlong.transform.localPosition.z);
    }
}
