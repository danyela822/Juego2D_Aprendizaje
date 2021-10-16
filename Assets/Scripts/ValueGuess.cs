using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueGuess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConstructStructure(int numLevels)
    {

        //mirar un valor final 
        int numBase = 2;

        List<List<Operand>> structure = new List<List<Operand>>();

        for (int i = 1; i <= numLevels; i++)
        {
            switch (i)
            {
                case 0: structure.Add(ConstructOperation(numBase, null));
                break;

                default: structure.Add(ConstructOperation(numBase + i, GetLastOperand(structure)));
                break;
            }
        }
    }

    public List<Operand> ConstructOperation(int numberNode, Operand firstNode){

        List<Operand> operation = new List<Operand>();
        for (int i = 0; i < numberNode; i++)
        {
            if (firstNode != null && i == 0)
            {
                operation.Add(firstNode);
            }
            if (i==numberNode -2)
            {
                //operation.Add(new Operand())
                
            }
        }

        return operation;
    }

    public Operand GetLastOperand(List<List<Operand>> structure){

        List<Operand> lastOperation = structure[structure.Count-1];
        Operand lastOperand = lastOperation[lastOperation.Count-1];

        return lastOperand;
    }
}
