using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Move(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

        transform.forward = targetPosition - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
