using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SuperGridCellProperties : ScriptableObject
{
    public int row; // row of bottom-most cell if it spans multiple
    public int col; // col of left-most cell if spans multiple

    public Vector2 center;
    public float zEstimate;

    public int k;
    public int refinedGridDimension;

    public float resolution;

    public float x0;
    public float y0;

    public int userID;

    public enum SurveyStatus { UNASSIGNED = 0, ASSIGNED = 1, INSPECTED = 2, QC_ASSIGNED = 3, COMPLETE = 4 };

    public SurveyStatus status;
}
