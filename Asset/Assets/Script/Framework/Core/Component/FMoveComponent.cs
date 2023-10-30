using UnityEngine;
using UnityEngine.Events;

public class FMoveComponent {
    public UnityAction OnMoveEvent;
    public GameObject targetGo;

    public FMoveComponent(GameObject targetGo) {
        this.targetGo = targetGo;
        OnMoveEvent = MoveEvent;
    }

    private void MoveEvent() {
        if(targetGo == null) {
            return;
        }

        if (Input.anyKey) {
            if (Input.GetKey(KeyCode.A)) {
                targetGo.transform.position += Vector3.left * Time.fixedDeltaTime * 5f;
            }
        }
    }
}