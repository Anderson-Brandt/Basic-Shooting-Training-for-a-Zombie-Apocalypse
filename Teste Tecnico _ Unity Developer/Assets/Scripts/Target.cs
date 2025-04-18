using UnityEngine;

public class Target : MonoBehaviour
{
    // public Animator anim;
    private float timeCount;
    private float timeCloseTarget = 3f;

    private void Start()
    {
        // anim = GetComponent<Animator>();
    }
    private void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeCloseTarget)
        {
            // anim.SetBool("Close", true);
            Destroy(gameObject, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destrói o alvo após o impacto
        Destroy(gameObject, 0.2f);
    }
}
