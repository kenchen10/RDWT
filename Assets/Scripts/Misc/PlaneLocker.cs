using UnityEngine;
using System.Collections;

public class PlaneLocker : MonoBehaviour {

    private Quaternion iniRot;
    private Vector3 iniPos;
 
    private void Start(){
        iniRot = transform.rotation;
        iniPos = transform.position;
    }
    
    private void LateUpdate(){
        transform.rotation = iniRot;
        transform.position = iniPos;
    }
}