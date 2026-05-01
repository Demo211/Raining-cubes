using UnityEngine;
using UnityEngine.Events;

public class Plane : MonoBehaviour
{
    public event UnityAction OnCollisionWithCube;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            if (cube.IsLiving == false)
            {
                cube.StartLifespanCountdown();
            }
        }
    }
}
