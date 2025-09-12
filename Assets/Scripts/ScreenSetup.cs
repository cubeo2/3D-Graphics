using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSetup : MonoBehaviour
{
    public GameObject[] gameObjects;

    public int pixelLength;
    public int pixelWidth;
    public float pixelSpacing;

    void Start()
    {

        // Create Pixels For Screen ------------
        float posX = 0;
        float posY = 0;
        Vector3 lastPos = new Vector3(posX, posY, 0);
        Vector3 nextPos = new Vector3(posX, posY, 0);

        MeshRenderer renderer = gameObjects[0].GetComponent<MeshRenderer>();
        Vector3 objSize = renderer.bounds.size;
        

        for (int i = 0; i < pixelLength; i++)
        {
            for(int j = 0; j < pixelWidth; j++)
            {
                nextPos = lastPos + new Vector3(objSize.x + pixelSpacing, 0,0);
                lastPos = nextPos;
                Instantiate(gameObjects[0], nextPos, Quaternion.identity);
            }
            nextPos = lastPos + new Vector3(0, objSize.y + pixelSpacing, 0) - new Vector3(pixelWidth *(objSize.x + pixelSpacing), 0, 0);
            lastPos = nextPos;
        }

        
    
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
