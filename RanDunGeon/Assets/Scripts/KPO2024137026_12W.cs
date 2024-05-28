using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KPO2024137026_12W : MonoBehaviour
{
    private void Start()
    {
        Car car = new Car();        //7.Page
        car.SetInTime();
        car.SetOutTime();

        System.Random random = new System.Random();         //9.Page ~
        Debug.Log(random.Next(10, 100));
        Debug.Log(random.Next(10, 100));
        Debug.Log(random.Next(10, 100));
        Debug.Log(random.Next(10, 100));
        Debug.Log(random.Next(10, 100));


        System.Random random_8_1 = new System.Random();         //변형예제 8-1

        Debug.Log(random_8_1.NextDouble() * 10);
        Debug.Log(random_8_1.NextDouble() * 10);
        Debug.Log(random_8_1.NextDouble() * 10);
        Debug.Log(random_8_1.NextDouble() * 10);
        Debug.Log(random_8_1.NextDouble() * 10);

        //리스트 변수 선언                 기본예제 5-3
        List<int> list = new List<int>();

        //리스트에 요소 추가
        list.Add(52);
        list.Add(273);
        list.Add(32);
        list.Add(64);

        foreach(var item in list)           //리스트 카운트 만큼 반복합니다.
        {
            Debug.Log("Count: " + list.Count + "\titem : " + item);
        }

        list.Remove(52);

        foreach (var item in list)           //리스트 카운트 만큼 반복합니다.
        {
            Debug.Log("Count: " + list.Count + "\titem : " + item);
        }


        Debug.Log(Mathf.Abs(-52273));             //기본예제 5-5
        Debug.Log(Mathf.Ceil(52.273f));
        Debug.Log(Mathf.Floor(52.273f));
        Debug.Log(Mathf.Max(52,273));
        Debug.Log(Mathf.Min(52,273));
        Debug.Log(Mathf.Round(52.273f));
    }

    class Car
    {
        int carNumber;
        int inTime;
        int outTime;

        public void SetInTime()
        {
            this.inTime = DateTime.Now.Hour;
        }

        public void SetOutTime()
        {
            this.outTime = DateTime.Now.Hour;
        }
    }
}
