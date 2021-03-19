using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnitMover : UnitMover
{
    [SerializeField] private float _maxDistanceFromTargetPosition;
    [SerializeField] private float _rotationTime;

    private void Update()
    {
        if (IsHaveDestination)
        {
            if (IsAtTargetPosition(TargetPosition) == false)
            {
                Vector3 target = TargetPosition;
                target.y = transform.position.y;
                float maxDistanceDelta = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
            }
            else if (TargetPosition == CurrentPath[CurrentPath.Length - 1].transform.position)
            {
                IsHaveDestination = false;
                TargetPositionIndex = 0;
                CurrentPath = null;
            }
            else
            {
                if (TargetPositionIndex + 1 != CurrentPath.Length)
                {
                    Vector3 target = CurrentPath[TargetPositionIndex + 1].transform.position - CurrentPath[TargetPositionIndex].transform.position;
                    //Debug.Log($"Current target way: {target}");
                    float newRotation = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;
                    //Debug.Log($"New Y rotation: {newRotation} degrees");
                    Quaternion newTargetRotation = Quaternion.Euler(new Vector3(0, newRotation, 0));
                    if (TargetRotation != newTargetRotation)
                    {
                        //Debug.Log("Changed TargetRotation");
                        TargetRotation = newTargetRotation;
                        StartCoroutine(ChangeToTargetRotation(_rotationTime));
                    }    

                    TargetPositionIndex++;
                    TargetPosition = CurrentPath[TargetPositionIndex].transform.position;
                    
                    //if (TargetRotation != CurrentPath[TargetPositionIndex].transform.rotation)
                    //{
                    //    TargetRotation = CurrentPath[TargetPositionIndex].transform.rotation;
                    //    StartCoroutine(ChangeRotation(TargetRotation, _rotationTime));
                    //    TargetRotation = Quaternion.identity;
                    //}
                }
            }
        }
    }

    private bool IsAtTargetPosition(Vector3 targetPosition)
    {
        if (transform.position.x + _maxDistanceFromTargetPosition >= targetPosition.x)
        {
            if (transform.position.z + _maxDistanceFromTargetPosition >= targetPosition.z)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator ChangeToTargetRotation(float rotationTime)
    {
        float passedTime = 0;
        Quaternion startRotation = transform.rotation;
        while (passedTime < rotationTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, TargetRotation, passedTime / rotationTime);
            passedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = TargetRotation;
    }

    //private IEnumerator ChangeRotation(Quaternion targetRotation, float rotationTime)
    //{
    //    float passedTime = 0;
    //    Quaternion startRotation = transform.rotation;
    //    while (passedTime < rotationTime)
    //    {
    //        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, passedTime / rotationTime);
    //        passedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.rotation = targetRotation;
    //}
}
