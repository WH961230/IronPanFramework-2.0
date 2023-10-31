using UnityEngine;

public class FLookComponent {
    public GameObject targetGo;

    public FLookComponent(GameObject targetGo) {
        this.targetGo = targetGo;
    }

    public void Look() {
        if(targetGo == null) {
            return;
        }

        if (Input.GetKey(KeyCode.Space)) {
            targetGo.transform.position += Vector3.up * Time.fixedDeltaTime * 5f;
        }
    }
}