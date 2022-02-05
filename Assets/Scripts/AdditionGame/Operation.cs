using System.Collections.Generic;
using UnityEngine;

public class Operation 
{
    public List<Operand> operands;
    public int resultado;
    readonly int possible_num = 8;
    readonly List<int> globalUsed = new List<int>();

    //contructor que permite realizar las demas operacions
    public Operation(int numberOperands, List<int> numUsed){

        globalUsed = AddAllInt(numUsed);
        operands = new List<Operand>();
        Build(numberOperands, numUsed);
        resultado = DoOperation();
        Debug.Log(OwnToString());
    }

    //contructor que permite realizar la primera operacion
    public Operation(int numberOperands, List<int> numUsed, int initialValue){   
        operands = new List<Operand>();
        Build(numberOperands, numUsed, initialValue);
        resultado = DoOperation();
    }

    //metodo que contruye las operaciones diferente a la primera
    public void Build (int numberOperands, List<int> integersUsed){
        //0perador 0 => no hay operador porque es el ultimo elemento
        //Operador 1 => resta
        //Operador 2 => suma
        int sign;
        int sum = 0;
        bool centinela = false;
        for (int i = 0; i < numberOperands; i++){
            if (i == numberOperands -1){
                int value = 0;
                while(centinela == false){
                    value = GetNewValue();
                    if (value < sum){
                        integersUsed.Add(value);
                        centinela = true;
                    }
                }                
                AddOperand(new Operand(value, 0));               
            }
            else{
                int usedValue = GetUsedValue();  
                RemoveUsed(usedValue);
                if(i == numberOperands - 2){
                    sum += usedValue;
                    sign = 1;
                }else{
                    sign = 2;
                    sum += usedValue;
                }
                AddOperand(new Operand(usedValue, sign));
            }

        }
    }

    //metodo que permite la construccion de la primera operacion
    public void Build (int numberOperands, List<int> integersUsed, int initialValue){

        bool centinelaSign = false;
        for (int i = 0; i < numberOperands; i++){
            if (i == 0){
                int sign = Random.Range(1, 3);
                if (sign == 1){
                    centinelaSign = true;
                }
                AddOperand(new Operand(initialValue, sign));
                integersUsed.Add(initialValue);
            }
            else{

                bool centinela = true;
                int value = GetNewValue();
                if (centinelaSign == true){

                    while(centinela == true){                    

                        if (initialValue - value >= 0){

                            AddOperand(new Operand(value, 0));
                            integersUsed.Add(value);
                            centinela = false;
                        }else{

                            value = GetNewValue();
                        }
                    }
                }else{
                    AddOperand(new Operand(value, 0));
                    integersUsed.Add(value);
                }
            }
        }
    }

    //metodo que permite obtenes un valor ya usado en lso 
    //operandos
    private int GetUsedValue (){
        bool noFound = true;
        int tentativeReturn = 0;
        while (noFound){
            int i = Random.Range(0, globalUsed.Count);
            tentativeReturn = globalUsed[i];
            if(!ContainsValue(tentativeReturn)) noFound = false;     
        }
        return tentativeReturn;
    }

    //metodo que permite saber si la lista ya tiene 
    //los valores random hallados
    public bool ContainsValue (int value){
        bool a = false;
        foreach (Operand operand in operands){
            if (operand.valueOperand == value) a = true;
        }
        return a;
    }

    //metodo que pemrite obtener valores nuevos para
    //los operandos
    public int GetNewValue(){
        bool noFound = true;
        int ret = 0;
        while (noFound){
            int value = Random.Range(0, possible_num);
            if (!ContainsValue(value)){
                noFound = false;
                ret = value;
            }
        }
        return ret;
    }

    //metodo que pemrite eliminar enteros de la lista global
    //optimizando codigo
    public void RemoveUsed(int valToRem){
        for (int i = 0; i < globalUsed.Count; i++){
            if (globalUsed[i] == valToRem){
                globalUsed.RemoveAt(i);
                break;
            }
        }
    }

    //metodo que pemrite agragar operandos
    public void AddOperand(Operand op){
        operands.Add(op);
    }

    public string OwnToString(){
        string ret = "(";
        foreach (Operand op in operands)
        {
            string valueS = "" + op.valueOperand;
            string sign = " ";
            if (op.operatorValue == 1) sign += "- ";
            if (op.operatorValue == 2) sign += "+ ";
            if (op.operatorValue == 0) sign += "= ";
            ret += valueS + sign;
        }

        return ret + resultado + ")";
    }

    //metodo que pemrite realizar la operacion y 
    //poder obtener el resultado
    public int DoOperation(){
        List<Operand> aux = AddAllOp();
        int ret = aux[0].valueOperand;
        int myOp = aux[0].operatorValue;
        Operand opAux = aux[0];
        aux.Remove(opAux);

        foreach (Operand op in aux)
        {
            int value = op.valueOperand;
            if(myOp == 1) ret -= value;
            if(myOp == 2) ret += value;
            myOp = op.operatorValue;
        }
        return ret;
    }

    //metodo que permite copiar un lista de operandos
    //en otra
    public List<Operand> AddAllOp(){

        List<Operand> a = new List<Operand>();
        foreach (Operand p in operands){
            a.Add(p);
        }
        return a;
    }

    //metodo que pemrite copiar un array enteros en otro 
    public List<int> AddAllInt(List<int> numbers){

        List<int> aux = new List<int>();
        foreach (int num in numbers){
            aux.Add(num);
        }
        return aux;
    }


    public List<Operand> Operands{
        get{
            return operands;
        }

        set{
            operands = value;
        }
    }
}
