using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Objectlevel2 : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] objects;
    public int[] droprd;


    public GameObject[] objects2;
    public GameObject[] blurobjects;
    public Vector2[] inipos;
    public int dropped;
    private int indicator;
    private Vector3 abc;
    public int itrator;
    // public GameObject self;
    // public GameObject up;
    // public GameObject down;
    public static System.Random r = new System.Random();
    public int scorevar = 0;
    // public Text newText;
    public int flag;

    void Start()
    {

        // self.SetActive(false);

        indicator = 0;
        render();
        randomize();
        itrator = 0;



        // System.Threading.Thread.Sleep(500);



        setInitialPosition();
        StartCoroutine(Example());



    }

    IEnumerator Example()
    {
        int x = indicator;

        yield return new WaitForSeconds(5);

        if (indicator != x)
        {
            yield break;
        }
        else
        {
            for (int j = indicator * 3; j < indicator * 3 + 3; j++)
            {
                if ((itrator * 3 + droprd[indicator]) < j)
                {
                    objects[j].SetActive(false);
                    blurobjects[j].SetActive(true);
                    blurobjects[j].transform.position = objects[j].transform.position;
                    // objects2[2].SetActive(false);
                }
                objects2[j].SetActive(false);

            }
        }






    }
    // IEnumerator Example2()
    // {
    //     print(Time.time);
    //     yield return new WaitForSeconds(0.3f);
    //     rot = rot + 10;
    //     objects2[0].transform.Rotate(new Vector3(0, rot, 0), Space.Self);
    //     // cards2[0].transform.Rotate(new Vector3(0, rot, 0), Space.Self);
    //     objects2[1].transform.Rotate(new Vector3(0, rot, 0), Space.Self);
    //     // cards2[1].transform.Rotate(new Vector3(0, rot, 0), Space.Self);
    //     objects2[2].transform.Rotate(new Vector3(0, rot, 0), Space.Self);
    //     // cards2[2].transform.Rotate(new Vector3(0, rot, 0), Space.Self);
    // }
    public void setInitialPosition()
    {
        int j = 0;
        int x = 0;
        // if(indicator > 4){
        // x = indicator % 4;
        // }else {
        x = indicator;
        // }
        for (int i = x * 3; i < x * 3 + 3; i++)
        {
            inipos[j] = objects[i].transform.position;
            j++;
        }
        j = 0;
    }


    public void randomize()
    {
        for (int i = 0; i < 15; i += 3)
        {

            List<int> termsList = new List<int>();
            List<int> termsList2 = new List<int>();

            for (int j = 0; j < 3; j++)
            {

                int pos = random_except_list(3, termsList.ToArray());
                termsList.Add(pos);
                termsList.Sort();
                objects[i + j].transform.position = cards[pos].transform.position;

                int pos2 = random_except_list(3, termsList2.ToArray());
                termsList2.Add(pos2);
                termsList2.Sort();
                objects2[i + j].transform.position = cards2[pos2].transform.position;
            }

        }
    }
    public void onclickright()
    {

        itrator++;

        if (indicator == 7)
        {
            // down.SetActive(true);
            // self.SetActive(false);
            // SceneManager.LoadScene("MatchingL3S1", LoadSceneMode.Single);
            return;
        }
        dropped = droprd[indicator];
        indicator++;
        if (indicator > 3)
        {
            randomize();
        }

        setInitialPosition();

        render();
        StartCoroutine(Example());


    }

    public void onclickleft()
    {
        itrator--;

        dropped = droprd[indicator];
        if (indicator != 0)
        {
            indicator--;
            if (indicator == 3)
            {
                randomize();
            }
            setInitialPosition();
            render();
            StartCoroutine(Example());

        }
        else
        {
            // up.SetActive(true);
            // self.SetActive(false);
        }

    }

    private void render()
    {
        for (int i = 0; i < 15; i++)
        {
            blurobjects[i].SetActive(false);
            if (i < 3)
            {
                cards2[i].SetActive(true);
            }
            objects[i].SetActive(false);
            objects2[i].SetActive(false);
            if ((indicator == 0) && (i >= 0 && i < 3))
            {
                objects[i].SetActive(true);
                objects2[i].SetActive(true);
            }
            else if ((indicator == 1) && (i >= 3 && i < 6))
            {
                objects[i].SetActive(true);
                objects2[i].SetActive(true);
            }
            else if ((indicator == 2) && (i >= 6 && i < 9))
            {
                objects[i].SetActive(true);
                objects2[i].SetActive(true);
            }
            else if ((indicator == 3) && (i >= 9 && i < 12))
            {
                objects[i].SetActive(true);
                objects2[i].SetActive(true);
            }
            else if ((indicator == 4) && (i >= 12 && i < 15))
            {
                objects[i].SetActive(true);
                objects2[i].SetActive(true);
            }
        }
    }



    public static int random_except_list(int n, int[] x)
    {
        int result = r.Next(n - x.Length);

        for (int i = 0; i < x.Length; i++)
        {
            if (result < x[i])
                return result;
            result++;
        }
        return result;
    }


    public void Drag(int ob)
    {
        if (objects[ob].transform.position == objects2[ob].transform.position)
        {
            return;
        }
        objects[ob].transform.position = Input.mousePosition;

    }
    public void drop(int a)
    {
        float Distance = Vector3.Distance(objects[a].transform.position, objects2[a].transform.position);
        if (Distance < 80)
        {


            objects[a].transform.position = objects2[a].transform.position;
            //  objects2[a].GetActive

            dropped++;

            objects2[a].SetActive(false);

            //  if(objects[a].transform.position==objects2[a].transform.position)
            // {

            //     return;
            // }


        }
        else
        {
            objects[a].transform.position = inipos[a % 3];
        }
        droprd[indicator] = dropped;

        Debug.Log(dropped);
        objects[dropped + (indicator * 3)].SetActive(true);

        blurobjects[dropped + (indicator * 3)].SetActive(false);

        if (indicator > 3)
        {
            if (dropped == 3)
            {

                onclickright();
            }
        }
        else
        {
            if (dropped == 3)
            {

                onclickright();
            }
        }


    }

    // void flaag()
    // {
    //     up.SetActive(true);
    //     GameObject thePlayer = GameObject.Find("LevelManager1");
    //     ObjectRenderer1 playerScript = thePlayer.GetComponent<ObjectRenderer1>();
    //     scorevar = playerScript.scorevar;
    //     Debug.Log(playerScript.scorevar);
    //     flag++;
    //     up.SetActive(false);
    // }





    // Update is called once per frame
    void Update()
    {

        // if (flag == 7)
        // {
        //     flaag();
        // }

    }
}
