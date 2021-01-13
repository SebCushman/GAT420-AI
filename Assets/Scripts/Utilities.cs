using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;

        // test result.x > max.x, if true result.x = min.x
        if(result.x > max.x)
        {
            result.x = min.x;
        }
        // test result.x < min.x, if true result.x = max.x
        if(result.x < min.x)
        {
            result.x = max.x;
        }

        if (result.y > max.y)
        {
            result.y = min.y;
        }
        if (result.y > max.y)
        {
            result.y = min.y;
        }

        if (result.z > max.z)
        {
            result.z = min.z;
        }
        if (result.z > max.z)
        {
            result.z = min.z;
        }

        return result;
    }

}
