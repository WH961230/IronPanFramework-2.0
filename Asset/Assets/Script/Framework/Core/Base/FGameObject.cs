using System.Collections.Generic;
using UnityEngine;

public class FGameObject {
    public GameObject GO;
    public Renderer GORENDERER;
    public Transform TRAN;
    public Vector3 TRAN_POS;
    public Vector3 TRAN_ROT;
    public Vector3 TRAN_SCALE;

    public FGameObject(GameObject GO, Transform TRAN) {
        this.GO = GO;
        this.TRAN = GO.transform;
        this.TRAN_POS = GO.transform.position;
        this.TRAN_ROT = GO.transform.rotation.eulerAngles;
        this.TRAN_SCALE = GO.transform.localScale;
        GORENDERER = GO.GetComponentInChildren<Renderer>();
    }

    public bool IsObjectRendered() {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(frustumPlanes, GORENDERER.bounds)) {
            return true;
        }

        return false;
    }
}