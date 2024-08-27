using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperGridCell : MonoBehaviour
{
    [SerializeField]
    SuperGridCellProperties properties = null;

    public void Initialize(SuperGridCellProperties properties)
    {
        this.properties = properties;
        name = "SuperGrid Cell " + properties.col + "-" + properties.row;
    }

    void Update()
    {
        if (properties != null)
        {
            transform.localPosition = new Vector3(properties.center.x, properties.center.y, properties.zEstimate);
            name = "SuperGrid Cell " + properties.col + "-" + properties.row;
        }
    }
}
