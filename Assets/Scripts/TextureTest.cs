using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;

//using System.Diagnostics;
//using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class TextureDrawing : MonoBehaviour
{
    [Header("Texture Resolution")]
    public int textureWidth = 256;
    public int textureHeight = 256;

    //Line Coordinates
    [Header("Line Coordinates")]
    public int Xi;
    public int Yi;
    public int Xf;
    public int Yf;
    private Texture2D texture;

    //Traingle Dimensions + Position
    [Header("Triangle Dimensions")]

    public int TriX1 = 0;
    public int TriY1 = 0;
    public int TriX2 = 0;
    public int TriY2 = 0;
    public int TriX3 = 0;
    public int TriY3 = 0;
    //public int TriBase = 0;
    //public int TriHeight = 0;
    //public float TriAngle= 0; // degrees    
    public Color  TriColor = Color.black;

    //Setting up the Space
    [Header("Space Setup")]
    public float boundaryX = 0;
    public float boundaryY = 0;
    public float boundaryZ = 0;

    [Header("Player Position Data")]
    public Vector3 playerPosition;
    public float relativeVY, relativeVX, relativeHZ;

    [Header("In Perspective Test")]
    public Vector3 p1 = new Vector3(0, 0, 0);
    public Vector3 p2 = new Vector3(0, 0, 0);
    public Vector3 p3 = new Vector3(0, 0, 0);

    //Frames Per Second
    [Header("Frame Rate")]
    public float FPS = 2;
    int frameCount = 0;
    public bool run = true;
    //float timeIN = 0;
    //float timeOUT = 1;
    //float currentTimeBTW = 0;
    float timeElapsed = 0;

    

    

    void Start()
    {

        // Create a blank texture
        texture = new Texture2D(textureWidth, textureHeight);
        texture.filterMode = FilterMode.Point;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;

        // create initial black texture
        ClearTexture(Color.black);

        // Apply the changes to the texture
        texture.Apply();
        
    }

    private void Update()
    {
        

        timeElapsed = timeElapsed + Time.deltaTime;

        if (timeElapsed >= (1.0f / FPS))
        {
            
            // Draw a red line
            ClearTexture(Color.black);
            //StaticAnimation();
            //DrawLine4(Xi, Yi, Xf, Yf, Color.green);
            //Triangle2(TriX1, TriY1, TriX2, TriY2, TriX3, TriY3, TriColor);
            InPerspective();

            texture.Apply();

            frameCount++;
            //Debug.Log($"Time Elapsed = {timeElapsed}");
            timeElapsed = 0;
        }
        //Debug.Log($"Time Elapsed = {timeElapsed}");
    }

    void InPerspective()
    {


        float viewPointY = playerPosition.y; //Vy
        float viewPointX = playerPosition.x - relativeVX; //Vx
        float viewHeight = playerPosition.z; //h

        Color colorTriangle = Color.green;
        Color colorHorizon = Color.white;

        //(viewPointX - p1.x)
        //Convert.ToInt32(viewPointX) +
        int y1 = Convert.ToInt32(((viewPointY - p1.y) / ( -p1.x)) * (playerPosition.x) + viewPointY);
        int z1 = Convert.ToInt32(((viewHeight - p1.z) / ( -p1.x)) * (playerPosition.x) + viewHeight);

        int y2 = Convert.ToInt32(((viewPointY - p2.y) / ( -p2.x)) * (playerPosition.x) + viewPointY);
        int z2 = Convert.ToInt32(((viewHeight - p2.z) / ( -p2.x)) * (playerPosition.x) + viewHeight);

        int y3 = Convert.ToInt32(((viewPointY - p3.y) / ( -p3.x)) * (playerPosition.x) + viewPointY);
        int z3 = Convert.ToInt32(((viewHeight - p3.z) / ( -p3.x)) * (playerPosition.x) + viewHeight);

        Triangle2(y1, z1, y2, z2, y3, z3, colorTriangle);
        DrawLine4(0, Convert.ToInt32(viewHeight), textureWidth, Convert.ToInt32(viewHeight), colorHorizon);

    }

    void Triangle2(int x1, int y1, int x2, int y2, int x3, int y3, Color color)
    {
        int[] coordinate = new int[] { x1, y1, x2, y2, x3, y3};

        for (int i = 0; i <= 4; i += 2)
        {

            int Xi = coordinate[i];
            int Yi = coordinate[i + 1];
            int Xf = 0;
            int Yf = 0;

            if (i == 4)
            {
                Xf = coordinate[0];
                Yf = coordinate[1];
            }
            else
            {
                Xf = coordinate[i + 2];
                Yf = coordinate[i + 3];
            }
            DrawLine4(Xi, Yi, Xf, Yf, color);
        }
    }

    void Triangle1 (int Base, int height, float angle, Color color)
    {
        //Notes on Triangle
        //  - All co-ordinates here are local
        //  - All co-ordinates are relative to the origin, which is the left most bottom corner
        //  - Angle must be greater than 0 and less than 180

        float TriAngleRads = 0;
        TriAngleRads = Mathf.Tan(angle * (Mathf.PI / 180));
        int x3 = Convert.ToInt32(TriAngleRads);

        int[] coordinate = new int[] {TriX1, TriY1, TriX1 + Base, TriY1, 0, TriY1 + height};

        string angleStr = "";

        if (angle > 0 && angle < 90)
        {
            coordinate[4] = -((TriY1 + height) / x3);

            angleStr = "0 to 90 Degrees";
        }
        else if (angle == 90)
        {
            coordinate[4] = TriY1 + height;

            angleStr = "90 Degrees";
        } 
        else if (angle > 90 && angle < 180)
        {
            //?????
            //int newAngle = 180 - angle;
            coordinate[4] = (TriY1 + height) / x3;

            angleStr = "90 - 180 Degrees";
        }
        else
        {
            angleStr = "Invalid Angle";
        }

        //Debug 1------
        int lineCount = 0;
        Debug.Log(angleStr + x3);
        //------

        for (int i = 0; i <= 4; i += 2)
        {
            //switch (i)
            //{
            //    case 0:

            //        break;


            //}

            int Xi = coordinate[i];
            int Yi = coordinate[i + 1];
            int Xf = 0;
            int Yf = 0;

            if( i == 4 )
            {
                Xf = coordinate[0];
                Yf = coordinate[1];
            }
            else
            {
                Xf = coordinate[i + 2];
                Yf = coordinate[i + 3];
            }
            DrawLine4(Xi, Yi, Xf, Yf, color);

            //Debug 1------
            lineCount++;
            //Debug.Log($"Lines drawn = {lineCount}");
            //------
        }
    }

    //Drawline 4 works, but should be optomized using Set Pixels!!!
    void DrawLine4(int x1, int y1, int x2, int y2, Color color)
    {
        int x = x1;
        int y = y1;

        //Variable Setup------------------------------------------
        //Vector Determination - Variables
        int xDiff = x2 - x1;
        int yDiff = y2 - y1;
        int xDiffABS = Math.Abs(xDiff);
        int yDiffABS = Math.Abs(yDiff);
        

        int diffOFdiff = xDiffABS - yDiffABS;

        //Direction Determination - Variables
        int xDirection = 0;
        int yDirection = 0;

        //Number of Moves total
        int totalMoves = xDiffABS + yDiffABS;
        int totalPixels = totalMoves + 1;

        //Setting up color Array
        int pixelGrid = xDiff * yDiff;

        //----------------------------------------------

        //Vertical && Horizontal Line Check and Direction Determination-----------------------
        if (xDiff == 0 && yDiff == 0)
        {
            texture.SetPixel(x1, y1, color);
            return;
        }
        else if (xDiff == 0)
        {
            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
        else if (yDiff == 0)
        {
            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
        }
        else
        {
            //Direction Check ------------------------------------
            //X axis
            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
            //Y axis
            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
                
        int pixelsDrawn = 0;
        int columnPosition = 1;
        int totNumColumns = xDiffABS + 1;

        // Debug Logs------------------------------------------------------------------------
        //Debug.Log("Drawline4 Method");
        //Debug.Log($"Diff-ABS in X = {xDiffABS}, Diff-ABS in Y = {yDiffABS}");
        //Debug.Log($"Change in X = {xDiff}, Absolute Change in X = {xDiffABS}");
        //Debug.Log($"Change in Y = {yDiff}, Absolute Change in Y = {yDiffABS}");
        //Debug.Log(totalMoves);
        //Debug.Log($"X Direction = {xDirection}");
        //Debug.Log($"Y Direction = {yDirection}");
        //Debug.Log($"Total# of Columns = {totNumColumns}");

        //Draw line------------------------------------------

        int xDiffTOT = 0,yDiffTOT = 0;

        for (int i = 1; i <= totalPixels; i++)
        {
            texture.SetPixel(x, y, color);
            pixelsDrawn++;

            if (xDiffTOT >= yDiffTOT)
            {
                yDiffTOT += yDiffABS;

                x += xDirection;
            }
            else
            {
                xDiffTOT += xDiffABS;

                y += yDirection;
            }

            //Debug Logs--------------------------------------------------------------
            //Debug.Log($"X Difference ABS = {xDiffABS}, Y Difference ABS = {yDiffABS}");
            

        }
        //Debug Logs-------------------------------------------------------------------
        //Debug.Log($"Number of Dots = {pixelsDrawn}");
        //Debug.Log($"Total Pixels = {totalPixels}");
        //Debug.Log($"X Difference ABS = {xDiffABS}, Y Difference ABS = {yDiffABS}");

    }

    void DrawLine3(int x1, int y1, int x2, int y2, Color color)
    {
        int x = x1;
        int y = y1;

        //Variable Setup------------------------------------------
        //Vector Determination - Variables
        int xDiff = x2 - x1;
        int yDiff = y2 - y1;
        int xDiffABS = Math.Abs(xDiff);
        int yDiffABS = Math.Abs(yDiff);

        int diffOFdiff = xDiffABS - yDiffABS;

        //Direction Determination - Variables
        int xDirection = 0;
        int yDirection = 0;

        //Number of Moves total
        int totalMoves = xDiffABS + yDiffABS;
        int totalPixels = totalMoves + 1;

        //----------------------------------------------
        // Debug Logs----------------------------------------------
        Debug.Log($"Change in X = {xDiff}, Absolute Change in X = {xDiffABS}");
        Debug.Log($"Change in Y = {yDiff}, Absolute Change in Y = {yDiffABS}");
        Debug.Log(totalMoves);

        //Vertical && Horizontal Line Check and Direction Determination-----------------------
        if (xDiff == 0 && yDiff == 0)
        {
            texture.SetPixel(x1, y1, color);
            return;
        }
        else if (xDiff == 0)
        {
            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
        else if (yDiff == 0)
        {
            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
        }
        else
        {
            //Direction Check ------------------------------------
            //X axis
            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
            //Y axis
            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }

        Debug.Log($"X Direction = {xDirection}");
        Debug.Log($"Y Direction = {yDirection}");

        int pixelsDrawn = 0;
        int columnPosition = 1;
        int totNumColumns = xDiffABS + 1;
        Debug.Log($"Total# of Columns = {totNumColumns}");
        //Draw line------------------------------------------
        for (int i = 1; i <= totalPixels; i++)
        {
            int numPixelPerRow = 0;
            if (xDiffABS >= yDiffABS)
            {
                numPixelPerRow = xDiffABS / yDiffABS;
            }
            else
            {
                numPixelPerRow = yDiffABS / xDiffABS;
            }
            //Debug.Log($"Number of pixels per row = {numPixelPerRow}");

            for (int j = 0; j < numPixelPerRow; j++)
            {
                texture.SetPixel(x, y, color);
                x += xDirection;
                xDiffABS += xDirection;
                pixelsDrawn++;
                totalPixels--;
            }

            y += yDirection;
            yDiffABS += yDirection;

            //if (totNumColumns * pixelsDrawn == totalPixels * columnPosition)
            //{
            //    x += xDirection;
            //    columnPosition++;
            //}
            //else
            //{
            //    y += yDirection;

            //}
            //Debug.Log($"Total# of Columns = {totNumColumns}");
            //Debug.Log($"Column Position = {columnPosition}");


            //totalMoves *= i;



            ////Check for Next Position - Store new Position
            //if (x1 == x2 && y1 == y2)
            //{
            //    i = totalMoves;
            //}
            //else if (xDiffABS >= yDiffABS) // if True move in X - Direction
            //{
            //    x += xDirection;
            //    xDiff = x2 - x;
            //    xDiffABS = Math.Abs(xDiff);
            //}
            //else //if False, move in Y Direction
            //{
            //    y += yDirection;
            //    yDiff = y2 - y;
            //    yDiffABS = Math.Abs(yDiff);
            //}

            //texture.SetPixel(x, y, color);
            //pixelsDrawn++;

        }
        Debug.Log($"Number of Dots = {pixelsDrawn}");

    }

    void DrawLine2(int x1, int y1, int x2, int y2, Color color)
    {
        int x = x1;
        int y = y1;

        //Variable Setup------------------------------------------
        //Vector Determination - Variables
        int xDiff = x2 - x1;
        int yDiff = y2 - y1;
        int xDiffABS = Math.Abs( xDiff );
        int yDiffABS = Math.Abs( yDiff );

        

        //Direction Determination - Variables
        int xDirection = 0;
        int yDirection = 0;

        //Number of Moves total
        int totalMoves = xDiffABS + yDiffABS;

        //----------------------------------------------
        // Debug Logs----------------------------------------------
        Debug.Log($"Change in X = {xDiff}, Absolute Change in X = {xDiffABS}");
        Debug.Log($"Change in Y = {yDiff}, Absolute Change in Y = {yDiffABS}");
        Debug.Log(totalMoves);

        //Vertical && Horizontal Line Check and Direction Determination-----------------------
        if (xDiff == 0 && yDiff == 0)
        {
            texture.SetPixel(x1, y1, color);
            return;
        }
        else if (xDiff == 0)
        {
            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
        else if (yDiff == 0)
        {
            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
        }
        else
        {
            //Direction Check ------------------------------------
            //X axis
            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
            //Y axis
            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
        Debug.Log($"X Direction = {xDirection}");
        Debug.Log($"Y Direction = {yDirection}");

        int count = 0;
        //Draw line------------------------------------------
        for (int i = 0; i < totalMoves; i++)
        {
            texture.SetPixel( x, y, color );
            //Check for Next Position - Store new Position
            if (x1 == x2 && y1 == y2)
            {
                i = totalMoves;
            }
            else if (xDiffABS >= yDiffABS) // if True move in X - Direction
            {
                x += xDirection;
                xDiff = x2 - x;
                xDiffABS = Math.Abs(xDiff);
            }
            else //if False, move in Y Direction
            {
                y += yDirection;
                yDiff = y2 - y;
                yDiffABS = Math.Abs( yDiff );
            }
            count ++;
            
        }
        Debug.Log($"Number of Dots = {count}");

    }
    void DrawLine(int x1, int y1, int x2, int y2, Color color)
    {
        //linear Equation---------------------
        int y = y1;
        int x = x1;

        int xDiff = x2 - x1;
        int yDiff = y2 - y1;
        int xDiffAbs = Math.Abs(xDiff);
        int yDiffAbs = Math.Abs(yDiff);

        //int valUse = 0;

        int xDirection = 0;
        int yDirection = 0;
        
        //Vertical && Horizontal Line Check-----------------------
        if (xDiff == 0 && yDiff == 0)
        {
            texture.SetPixel(x1, y1, color);
            return;
        }
        else if (xDiff == 0 )
        {
            //valUse = yDiff;

            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        } 
        else if(yDiff == 0)
        {
            //valUse = xDiff;

            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
        }
        else
        {
            //valUse = xDiff;

            //int m = (y2 - y1) / (x2 - x1);
            //int b = y - m * x;

            //Direction Check ------------------------------------
            //X axis
            if (xDiff < 0)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
            //Y axis
            if (yDiff < 0)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }

        //Draw Line----------------------------
        for (int i = 0; i < xDiffAbs; i ++)
        {
            //texture.SetPixel(x, y, color);
            for (int j = 0; j < yDiffAbs; j++)
            {
                texture.SetPixel(x, y, color);
                y += yDirection;
            }
            //y += yDirection;
            x += xDirection;
        }
    }

    void DrawLineComp(int x0, int y0, int x1, int y1, Color color)
    {
        int dx = Mathf.Abs(x1 - x0), dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1, sy = y0 < y1 ? 1 : -1; //Direction determination
        int err = dx - dy;

        while (true)
        {
            texture.SetPixel(x0, y0, color); // Set the pixel at (x0, y0)

            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;

            if (e2 > -dy) 
            { 
                err -= dy;
                x0 += sx; //move x in correct direction
            }
            if (e2 < dx) 
            { 
                err += dx; 
                y0 += sy; //move y in correct direction
            }
        }
    }
    void StaticAnimation()
    {
        Color[] pixels = new Color[textureWidth * textureHeight];

        for (int i = 0; i < pixels.Length; i++)
        {
            int rand = UnityEngine.Random.Range(0, 1);

            if (rand == 0) { pixels[i] = Color.black; }
            else { pixels[i] = Color.white; }
        }
        texture.SetPixels(pixels);

        //Cool pottential for static-----------------------

        ////linear Equation---------------------
        //int y = y1;
        //int x = x1;

        //int xDiff = x2 - x1;
        //int yDiff = y2 - y1;
        //int xDiffAbs = Math.Abs(xDiff);
        //int yDiffAbs = Math.Abs(yDiff);

        ////int valUse = 0;

        //int xDirection = 0;
        //int yDirection = 0;

        ////Vertical && Horizontal Line Check-----------------------
        //if (xDiff == 0 && yDiff == 0)
        //{
        //    texture.SetPixel(x1, y1, color);
        //    return;
        //}
        //else if (xDiff == 0)
        //{
        //    //valUse = yDiff;

        //    if (yDiff < 0)
        //    {
        //        yDirection = -1;
        //    }
        //    else
        //    {
        //        yDirection = 1;
        //    }
        //}
        //else if (yDiff == 0)
        //{
        //    //valUse = xDiff;

        //    if (xDiff < 0)
        //    {
        //        xDirection = -1;
        //    }
        //    else
        //    {
        //        xDirection = 1;
        //    }
        //}
        //else
        //{
        //    //valUse = xDiff;

        //    //int m = (y2 - y1) / (x2 - x1);
        //    //int b = y - m * x;

        //    //Direction Check ------------------------------------
        //    //X axis
        //    if (xDiff < 0)
        //    {
        //        xDirection = -1;
        //    }
        //    else
        //    {
        //        xDirection = 1;
        //    }
        //    //Y axis
        //    if (yDiff < 0)
        //    {
        //        yDirection = -1;
        //    }
        //    else
        //    {
        //        yDirection = 1;
        //    }
        //}

        ////Draw Line----------------------------
        //for (int i = 0; i < xDiffAbs; i++)
        //{
        //    for (int j = 0; j < yDiffAbs; j++)
        //    {
        //        texture.SetPixel(x, y, color);
        //        y += yDiff;
        //    }
        //    x += xDiff;
        //}
    }
    void ClearTexture(Color color)
    {
        Color[] pixels = new Color[textureWidth * textureHeight];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }
        texture.SetPixels(pixels);
    }
}
