using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private bool isMoving = false;
    public float moveDelay = 2f;
    [SerializeField] private MapGenerator gen;

    private void Update()
    {
        if (!isMoving)
            StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        isMoving = true;
        yield return new WaitForSeconds(moveDelay);
        float totalMovementTime = 200f;
        float currMovenentTime = 0f;
        Vector3 dest = transform.position + transform.right * 0.06f;
        while (Vector3.Distance(transform.position, dest) > 0.0001f)
        {
            currMovenentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dest, currMovenentTime / totalMovementTime);
            yield return null;
        }
        isMoving = false;
        gen.Generate();
    }
}
