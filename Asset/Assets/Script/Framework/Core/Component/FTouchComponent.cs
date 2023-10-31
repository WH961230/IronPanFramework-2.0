using System;
using UnityEngine;

public class FTouchComponent : MonoBehaviour {
    public GameObject targetGo;
    public Collider targetCollider;
    public string targetColliderName;

    public FTouchComponent(GameObject targetGo) {
        this.targetGo = targetGo;
        targetCollider = targetGo.GetComponent<Collider>();
        targetCollider.isTrigger = true;
    }

    public void Touch() {
        if(targetGo == null || targetCollider == null || String.IsNullOrEmpty(targetColliderName)) {
            return;
        }

        Debug.Log("Touch: " + targetColliderName);
    }

    public void OnTriggerEnter(Collider other) {
        targetColliderName = other.name;
    }
}