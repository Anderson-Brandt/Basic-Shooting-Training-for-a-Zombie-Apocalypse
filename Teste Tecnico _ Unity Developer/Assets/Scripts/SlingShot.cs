using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public List<GameObject> shotsPrefabs = new List<GameObject>();
    public Transform shotSpawnPoint;
    public float slingShotForce = 10f;

    public Transform leftBandPoint;
    public Transform rightBandPoint;
    public LineRenderer leftBand;
    public LineRenderer rightBand;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private bool isDragging = false;

    private GameObject currentProjectile;
    private Rigidbody2D currentRb;

    public bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;

            // Instanciar a pedra no toque
            currentProjectile = Instantiate(shotsPrefabs[Random.Range(0, shotsPrefabs.Count)], shotSpawnPoint.position, Quaternion.identity);
            currentRb = currentProjectile.GetComponent<Rigidbody2D>();
            currentRb.bodyType = RigidbodyType2D.Kinematic;
            // Impede que ela caia durante o arrasto
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateBands(dragPos);

            // Atualiza posição da pedra
            if (currentProjectile != null)
            {
                currentProjectile.transform.position = dragPos;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Shot();
            ClearBands();
            AudioController.current.PlayMusic(AudioController.current.slingShot);
            isDragging = false;
        }
    }

    void Shot()
    {
        Vector2 direction = startTouchPos - endTouchPos;

        if (currentRb != null)
        {
            currentRb.bodyType = RigidbodyType2D.Dynamic;
            currentRb.AddForce(direction * slingShotForce, ForceMode2D.Impulse);
        }

        currentProjectile = null;
        currentRb = null;
    }

    void UpdateBands(Vector2 dragPosition)
    {
        leftBand.positionCount = 2;
        rightBand.positionCount = 2;

        leftBand.SetPosition(0, leftBandPoint.position);
        leftBand.SetPosition(1, dragPosition);

        rightBand.SetPosition(0, rightBandPoint.position);
        rightBand.SetPosition(1, dragPosition);
    }

    void ClearBands()
    {
        leftBand.positionCount = 0;
        rightBand.positionCount = 0;
    }
}




