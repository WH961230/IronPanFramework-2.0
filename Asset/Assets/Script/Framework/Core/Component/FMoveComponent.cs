using UnityEngine;
using UnityEngine.Events;

public class FMoveComponent {
    public UnityAction OnMoveEvent;
    public FGameObject targetFGo;

    public FMoveComponent(FGameObject targetFGo) {
        this.targetFGo = targetFGo;
        OnMoveEvent = MoveEvent;
    }

    private void MoveEvent() {
        if(targetFGo == null) {
            return;
        }

        if (Input.anyKey) {
            if (Input.GetKey(KeyCode.A)) {
                targetFGo.TRAN_POS += Vector3.left * Time.fixedDeltaTime * 5f;
            }
        }

        if (targetFGo.IsObjectRendered()) {
            if (targetFGo.TRAN_POS != targetFGo.GO.transform.position) {
                targetFGo.GO.transform.position = targetFGo.TRAN_POS;
            }
        }
    }
}