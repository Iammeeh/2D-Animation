using UnityEngine;

public class TestDummy : MonoBehaviour
{
    public int health = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MeleeAttack"))
        {
            Debug.Log("WHAT IN THE BLOODY HELLS WERE YOU THINKING I WAS RIGHT THERE!");
            health -= 10;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
