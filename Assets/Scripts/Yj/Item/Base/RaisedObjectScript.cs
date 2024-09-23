using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RaisedObjectScript : MonoBehaviour
{
    public GameObject raisedObj;
    public void RaisObject(GameObject _raisedObj)
    {
        raisedObj = _raisedObj;
        raisedObj.transform.SetParent(transform);
        Vector3 raisedObjPos = new Vector3(transform.position.x,
                                            transform.position.y + 0.005f,
                                            transform.position.z);
        raisedObj.transform.position = raisedObjPos;
        raisedObj.transform.rotation =  Quaternion.identity;
    }

    public GameObject GetRaisedObject()
    {
        return raisedObj;
    }

    public bool IsRaised()
    {
        if (raisedObj == null)
            return false;
        else
            return true;
    }

    public void Release()
    {
        raisedObj = null;
        Debug.Log("올린 물품 제거");
    }

    public GameObject GetTopRaisedObj()
    {
        GameObject current = raisedObj;
        while (current != null)
        {
            if(!current.GetComponent<RaisedObjectScript>())
            {
                return current;
            }
            if (current.GetComponent<RaisedObjectScript>().GetRaisedObject() == null)
            {
                return current;  //가장 위에 있는 오브젝트는 올려둔게 없으므로 null임을 사용
            }
            current = current.GetComponent<RaisedObjectScript>().GetRaisedObject();
        }
        return this.gameObject; // 위에 있는 것이 없음
    }
    public RaisedObjectScript GetTopRaisedScr()
    {
        RaisedObjectScript raisedObjScr = this;
        while (raisedObjScr != null)
        {
            if (!raisedObjScr.GetRaisedObject().GetComponent<RaisedObjectScript>())
            {
                return raisedObjScr;
            }
            raisedObjScr = raisedObjScr.GetRaisedObject().GetComponent<RaisedObjectScript>();
        }
        return raisedObjScr; // 위에 있는 것이 없음
    }
}
