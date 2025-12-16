using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        BirdController bird = collision.GetComponent<BirdController>();

        if (bird != null)
        {
            
            GameManager gm = FindFirstObjectByType<GameManager>();

            if (gm != null)
            {
                gm.AddScore();
             
            }
         
        }
       
    }
}