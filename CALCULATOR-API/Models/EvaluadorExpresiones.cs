using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CALCULATOR_API.Models
{
    static class EvaluatorExpressions
    {
        static private Stack<string> pila = new Stack<string>();
        static private List<string> operadores = new List<string> { "*", "/", "+", "-" };

        static private decimal CalcularOperacion(decimal x, decimal y, string operador)
        {
            decimal resultado = 0;

            switch (operador)
            {
                case "+":
                    {
                        resultado = x + y;
                        break;
                    }
                case "-":
                    {
                        resultado = x - y;
                        break;
                    }
                case "*":
                    {
                        resultado = x * y;
                        break;
                    }
                case "/":
                    {
                        resultado = x / y;
                        break;
                    }
                default:
                    break;
            }

            return resultado;
        }

        static private List<string> StringToList(string expresion)
        {
            var listaExpresion = new List<string>();
            int indiceListaExpresion = 0;

            listaExpresion.Add(expresion[0].ToString());

            for (int i = 1; i < expresion.Length; i++)
            {
                var valor = expresion[i];

                if (char.IsDigit(valor) && operadores.Concat(new string[2] { "(", ")" }).Contains(listaExpresion.ElementAt(indiceListaExpresion)))
                {
                    listaExpresion.Add(valor.ToString());
                    indiceListaExpresion++;
                }
                else if (operadores.Concat(new string[2] { "(", ")" }).Contains(valor.ToString()))
                {
                    listaExpresion.Add(valor.ToString());
                    indiceListaExpresion++;
                }
                else
                {
                    var numero = listaExpresion.ElementAt(indiceListaExpresion) + valor;

                    listaExpresion.RemoveAt(indiceListaExpresion);
                    listaExpresion.Add(numero);
                }
            }

            return listaExpresion;
        }

        static private int PrecedenciaOperador(string operador)
        {
            int precedencia = 0;

            if (operador == "*" || operador == "/")
            {
                precedencia = 1;
            }
            else if (operador == "+" || operador == "-")
            {
                precedencia = 2;
            }

            return precedencia;
        }

        static public string InFijaToPostFija(string expresionInfija)
        {
            string expresionPostFija = "";
            decimal numero = 0;

            expresionInfija = expresionInfija.Replace(" ", "");

            var listaExpresionInfija = StringToList(expresionInfija);

            pila.Push("(");
            listaExpresionInfija.Add(")");

            foreach (var x in listaExpresionInfija)
            {
                if (decimal.TryParse(x, out numero))
                {
                    expresionPostFija += x + " ";
                }
                else if (x == "(")
                {
                    pila.Push(x.ToString());
                }
                else if (operadores.Contains(x.ToString()))
                {
                    var operadorPila = pila.Peek();

                    int precedenciaOperadorPila = PrecedenciaOperador(operadorPila);
                    int precedenciaOperadorExpresion = PrecedenciaOperador(x.ToString());

                    while (precedenciaOperadorPila <= precedenciaOperadorExpresion && operadores.Contains(operadorPila))
                    {
                        expresionPostFija += pila.Pop() + " ";
                        operadorPila = pila.Peek();
                        precedenciaOperadorPila = PrecedenciaOperador(operadorPila);
                    }

                    pila.Push(x.ToString());
                }
                else if (x == ")")
                {
                    while (pila.Peek() != "(")
                    {
                        expresionPostFija += pila.Pop() + " ";
                    }

                    pila.Pop();
                }
            }

            return expresionPostFija;
        }

        static public decimal EvaluarExpresionPostFija(string expresionInfija) 
        {

            string expresionPostfija = InFijaToPostFija(expresionInfija);
            decimal op1, op2 = 0;
            var listaPostFija = expresionPostfija.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            decimal numero = 0;

            foreach (var x in listaPostFija)
            {
                if (decimal.TryParse(x, out numero))
                {
                    pila.Push(x);
                }
                else if (operadores.Contains(x))
                {
                    op1 = decimal.Parse(pila.Pop());
                    op2 = decimal.Parse(pila.Pop());
                    pila.Push(CalcularOperacion(op2, op1, x).ToString());
                }
            }

            return decimal.Parse(pila.Pop());
        }
    }
}
