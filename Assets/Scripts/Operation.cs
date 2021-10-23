using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operation 
{
    public List<Operand> operands;
    public int resultado;
    int possible_num = 20;
    List<int> globalUsed = new List<int>();
    

    public Operation(int numberOperands, List<int> numUsed){

        globalUsed = AddAllInt(numUsed);
        operands = new List<Operand>();
        Build(numberOperands, numUsed);
        resultado = DoOperation();
        OwnToString();

    }

    //contructor que permite realizar la primera operacion
    public Operation(int numberOperands, List<int> numUsed, int initialValue){
        
        operands = new List<Operand>();
        Build(numberOperands, numUsed, initialValue);
        resultado = DoOperation();
        OwnToString();
    }

    public void Build (int numberOperands, List<int> integersUsed){
        //0perador 0 => no hay operador porque es el ultimo elemento
        //Operador 1 => resta
        //Operador 2 => suma
        for (int i = 0; i < numberOperands; i++){
            if (i == numberOperands -1){
                int value = GetNewValue();
                integersUsed.Add(value);
                AddOperand(new Operand(value, 0));               
            }
            else{
                int usedValue = GetUsedValue();
                RemoveUsed(usedValue);
                int sign = Random.Range(1, 3);
                AddOperand(new Operand(usedValue, sign));
            }

        }
    }

    public void Build (int numberOperands, List<int> integersUsed, int initialValue){

        for (int i = 0; i < numberOperands; i++){
            if (i == 0){
                int sign = Random.Range(1, 3);
                AddOperand(new Operand(initialValue, sign));
                integersUsed.Add(initialValue);
            }
            else{
                int value = GetNewValue();
                AddOperand(new Operand(value, 0));
                integersUsed.Add(value);
            }
        }
    }
    private int GetUsedValue (){
        bool noFound = true;
        int tentativeReturn = 0;
        while (noFound)
        {
            int i = Random.Range(0, globalUsed.Count);
            tentativeReturn = globalUsed[i];
            if(!ContainsValue(tentativeReturn)) noFound = false;     
        }

        return tentativeReturn;
    }

    public bool ContainsValue (int value){
        foreach (Operand operand in operands){
            if (operand.valueOperand == value) return true;
        }
        return false;
    }

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

    public void RemoveUsed(int valToRem){
        for (int i = 0; i < globalUsed.Count; i++){
            if (globalUsed[i] == valToRem){
                globalUsed.Remove(i);
                break;
            }
        }
    }

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
    public List<Operand> AddAllOp(){

         List<Operand> a = new List<Operand>();

        foreach (Operand p in operands)
        {
            a.Add(p);
        }

        return a;
    }

    public List<int> AddAllInt(List<int> numbers){

        List<int> aux = new List<int>();

        foreach (int num in numbers)
        {
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
