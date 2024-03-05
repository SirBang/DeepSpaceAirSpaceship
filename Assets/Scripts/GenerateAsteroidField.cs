using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroidField : MonoBehaviour
{

    public Transform asteriodPrefab;
    public int fieldRadius = 1000;
    public int asteroidCount = 10000;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < asteroidCount ; i++){
            // Instantiate the asteroid
            Transform asteroidInstance = Instantiate(asteriodPrefab, Random.insideUnitSphere * fieldRadius, Quaternion.identity);

            // Generate a random size
            float size = Random.Range(0.5f, 2.0f); // Adjust min and max size as needed
            asteroidInstance.localScale = new Vector3(size, size, size); // Set the random size
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
