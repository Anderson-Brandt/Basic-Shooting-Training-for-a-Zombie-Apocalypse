using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactParticlePrefab;

    public int countHumanShot;
    public int countEnemyShot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Instancia a partícula no ponto de impacto
        Instantiate(impactParticlePrefab, transform.position, Quaternion.identity);
        // Destrói a pedra após o impacto
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Human"))
        {
            AudioController.current.PlayMusic(AudioController.current.humanShot);
            countHumanShot++;
            GameManager.instance.lifesCount += countHumanShot;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioController.current?.PlayMusic(AudioController.current.zombieShot);
            countEnemyShot++;
            GameManager.instance.score += countEnemyShot;
        }
    }
}
