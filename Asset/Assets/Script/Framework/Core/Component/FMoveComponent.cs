using UnityEngine;

public class FMoveComponent {
    public GameObject targetGo;

    public FMoveComponent(GameObject targetGo) {
        this.targetGo = targetGo;
    }

    public void Move() {
        if(targetGo == null) {
            return;
        }

        if (Input.GetKey(KeyCode.A)) {
            targetGo.transform.position += Vector3.left * Time.fixedDeltaTime * 5f;
        } else if (Input.GetKey(KeyCode.D)) {
            targetGo.transform.position += Vector3.right * Time.fixedDeltaTime * 5f;
        } else if (Input.GetKey(KeyCode.W)) {
            targetGo.transform.position += Vector3.forward * Time.fixedDeltaTime * 5f;
        } else if (Input.GetKey(KeyCode.S)) {
            targetGo.transform.position += Vector3.back * Time.fixedDeltaTime * 5f;
        }
    }
}