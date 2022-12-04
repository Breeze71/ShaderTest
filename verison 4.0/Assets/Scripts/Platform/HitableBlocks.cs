////
using UnityEngine.Events;
using UnityEngine;

public class HitableBlocks : MonoBehaviour
{
    [SerializeField] private UnityEvent _hit;

    private void OnCollisionEnter2D(Collision2D other){

        var player = other.collider.GetComponent<Movement>();

        if(player && other.contacts[0].normal.y > 0){

            _hit?.Invoke();
        }

    }


}
