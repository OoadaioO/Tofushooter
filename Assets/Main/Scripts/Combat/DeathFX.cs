using UnityEngine;

public class DeathFX : MonoBehaviour
{
    
    public void OnAnimationEnd(){
        gameObject.SetActive(false);
    }

    
}
