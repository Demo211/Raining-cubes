using UnityEngine;

public class Plane : MonoBehaviour
{
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
