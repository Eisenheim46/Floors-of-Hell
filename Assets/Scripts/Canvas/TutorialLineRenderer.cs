using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLineRenderer : MonoBehaviour
{

    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;

    private LineRenderer lineDraw;

    // Start is called before the first frame update
    void Start()
    {
        lineDraw = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lineDraw.SetPosition(0, PointA.position);
        lineDraw.SetPosition(1, PointB.position);
    }
}
