using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    GameObject[,] o = new GameObject[5, 6];
    public GameObject [,] Obj
    {
        get { return o; }
        set { o = value; }
    }
    int[,] f = new int[5, 6];
    public int [,] Field
    {
        get { return f; }
        set { f = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckPos(Vector2 P1, Vector2 P2)
    {
        float x = P1.x - P2.x;
        float y = P1.y - P2.y;
        float r = Mathf.Sqrt(x * x + y * y);
        if(r < 93.75f)
        {
            return true;
        }

        return false;
    }

    public void ChangePos(GameObject obj1, GameObject obj2)
    {
        DropCnt d1 = obj1.GetComponent<DropCnt>();
        DropCnt d2 = obj2.GetComponent<DropCnt>();
        GameObject tempObj;
        Vector2 tempPos;
        int temp;
        tempObj = Obj[d1.ID1, d1.ID2];
        Obj[d1.ID1, d1.ID2] = Obj[d2.ID1, d2.ID2];
        Obj[d2.ID1, d2.ID2] = tempObj;

        temp = Field [d1.ID1, d1.ID2];
        Field[d1.ID1, d1.ID2] = Field[d2.ID1, d2.ID2];
        Field[d2.ID1, d2.ID2] = temp;

        tempPos = d1.P1;
        d1.P1 = d2.P1;
        d2.P1 = tempPos;
        tempPos = d1.P2;
        d1.P2 = d2.P2;
        d2.P2 = tempPos;

        temp = d1.ID1;
        d1.ID1 = d2.ID1;
        d2.ID1 = temp;

        temp = d1.ID2;
        d1.ID2 = d2.ID2;
        d2.ID2 = temp;
    }

    public void DeleteDrop()
    {
        int c = 0, t = 0;
        int[,] temp = new int[5, 6];
        int[,] temp2 = new int[5, 6];

        for(int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if(j == 0)
                {
                    c = 1;
                    t = Field[i, j];
                    continue;
                }
                if(t == Field[i, j])
                {
                    c++;
                    if(c>=3)
                    {
                        temp[i, j] = c; 
                    }
                }
                else
                {
                    c = 1;
                    t = Field[i, j];
                }
            }
        }

        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    c = 1;
                    t = Field[i, j];
                    continue;
                }
                if (t == Field[i, j])
                {
                    c++;
                    if (c >= 3)
                    {
                        temp2[i, j] = c;
                    }
                }
                else
                {
                    c = 1;
                    t = Field[i, j];
                }
            }
        }

        for(int i = 0; i < 5; i++)
        {
            for(int j= 0; j < 6; j++)
            {
                if (temp[i,j] >= 3)
                {
                    for (int k = j; temp[i, j] > 0; k--, temp[i, j]--)
                    {
                        Field[i, k] = 6;
                        Obj[i, k].GetComponent<DropCnt>().Set(6);
                    }
                }

                if (temp2[i,j] >= 3)
                {
                    for(int k = i; temp2[i, j] > 0; k--,temp2[i,j]--)
                    {
                        Field[k, j] = 6;
                        Obj[k, j].GetComponent<DropCnt>().Set(6);
                    }
                }
            }
        }
    }

    public void DownDrop()
    {
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                if(Field[i, j] == 6)
                {
                    for(int k = i; k > 0; k--)
                    {
                        if(Field[k-1, j] != 6)
                        {
                            ChangePos(Obj[k, j], Obj[k - 1, j]);
                        }
                    }
                }
            }
        }
    }

    public void ResetDrop()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if(Field[i, j] == 6)
                {
                    int type = Random.Range(0, 6);
                    Field[i, j] = type;
                    Obj[i, j].GetComponent<DropCnt>().Set(type);
                }
            }
        }
    }

    public bool Check()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (Field[i, j] == 6)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
