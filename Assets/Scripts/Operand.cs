using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operand 
{

    //valos de la operacion
    public int valueOperand;
    //operador = 1 resta
    //operador = 2 suma
    public int operatorValue;

    //contructor de la clase
    public Operand(int valueOperand, int operatorValue){
        this.valueOperand = valueOperand;
        this.operatorValue = operatorValue;
    }

    public int OperatorValue{
        get{
            return operatorValue;
        }

        set{

            operatorValue = value;
        }
    }

    public int ValueOperand{
        get{
            return valueOperand;
        }

        set{
            valueOperand = value;
        }
    }

    //PENDIENDTE LOS GET Y SET
    //NO ESTOY SEGURA SI LOS NECESITOs
}
