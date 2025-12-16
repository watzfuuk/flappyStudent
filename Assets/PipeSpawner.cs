using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab; // Сюди перетягнеш префаб PipePair
    public float timeToSpawn = 2f; // Час між трубами
    private float timer = 0;
    public float heightRange = 1.5f; // Розкид висоти

    void Update()
    {
        if (timer > timeToSpawn)
        {
            SpawnPipe();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void SpawnPipe()
    {
        // Створюємо нову трубу
        GameObject newPipe = Instantiate(pipePrefab);
        // Ставимо її в позицію спавнера + випадкова висота
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange), 0);
    }
}