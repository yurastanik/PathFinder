﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functionclass {

    //public int btn_num = 0;
    public List <Input_Class> input_arr;

    public Functionclass(int btn_num) {
        input_arr = new List<Input_Class>();
        for (int i = 0; i < btn_num; i++) {
            input_arr.Add(new Input_Class(0, 0));
        }
    }

}
