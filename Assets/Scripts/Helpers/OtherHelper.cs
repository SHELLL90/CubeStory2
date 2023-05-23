using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OtherHelper 
{
    public static bool CheckAvailability(Vector2 point, GameObject gameObject, LayerMask layerMask)
    {
        Vector2 direction = (Vector2)gameObject.transform.position - point;
        float distance = Vector2.Distance(point, gameObject.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(point, direction, distance, layerMask.value);
        if (hit)
        {
            if (hit.transform.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }
    public static float GetDistance(Transform firstTransform, Transform secondTransform)
    {
        return Vector2.Distance(firstTransform.position, secondTransform.position);
    }
}
