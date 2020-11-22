
using System;
using System.IO;
using System.Diagnostics;


namespace Simplex_Algorithm
{
    public static class Constants
    {
        //Declaração das constantes fundamentais do programa.
        public static int qtdVariable = 0;
        public static int line = 0;
        public static int column = 0;
        public static int qtdeBases = 0;
    }
    class Program
    {

        //Variável do tipo string contendo o caminho do arquivo txt
        private static string filePath = @"../../../exemple.txt";

        static void Main(string[] args)
        {
            //Declaração das variáveis que irão compor as informações do arquivo txt.
            double[,] prinMatrix;
            int[] bases;
            int i, j, cont, baseIndex = 0, interacao = 0;
            double aux, pivot;


            //Função que retorna o total de linhas do txt
            Constants.line = returnLines();

            //Função que retorna o total de colunas do txt
            Constants.column = returnColumns();

            //Moldando o tamanho das variáveis ao tamanho dos dados pegos no txt
            prinMatrix = new double[Constants.line, Constants.column];
            bases = new int[Constants.line - 1];

            //Função para a coleta dos dados do txt
            returnDataTxt(prinMatrix);

            //Atribuição dos valores 
            for (i = 0; i < Constants.line - 1; i++)
            {
                bases[i] = i + Constants.qtdVariable + 1;
            }

            //Printa a tabela inteira montada
            Console.WriteLine("\n\nTabela Montada\n-----------------------------------------------------------------\n");


            for (i = 0; i < Constants.line; i++)
            {
                for (j = 0; j < Constants.column; j++)
                {
                    Console.Write(prinMatrix[i, j].ToString("F"));
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\n-----------------------------------------------------------------\n");

            while (searchNegative(prinMatrix) == 0)
            {
                interacao += 1;
                int smallNumber;
                int NegativeColumn;
                double auxIndex = Double.PositiveInfinity;
                cont = 0;


                //--------------------Lógica de seleção para achar o Pivot----------------------//
                for (i = 0; i < Constants.column; i++)
                {
                    if (prinMatrix[Constants.line - 1, i] < auxIndex)
                    {
                        auxIndex = prinMatrix[Constants.line - 1, i];
                        cont = i;
                    }

                }
                //-------------------------------------------------------------------------------
                NegativeColumn = cont;

                auxIndex = double.PositiveInfinity;

                //--------------------Lógica que divide tudo pelo Pivot----------------------//
                for (i = 0; i < Constants.line - 1; i++)
                {
                    aux = prinMatrix[i, Constants.column - 1] / prinMatrix[i, NegativeColumn];

                    if (aux < auxIndex)
                    {
                        auxIndex = aux;
                        baseIndex = i;
                    }
                }
                smallNumber = baseIndex;
                //---------------------------------------------------------------------------------------------//

                //Troca dos X
                bases[smallNumber] = (NegativeColumn + 1);


                //Lógica que irá fazer com que todos os valores da linha escolhida seja divido pelo Pivot
                pivot = prinMatrix[smallNumber, NegativeColumn];

                for (i = 0; i < Constants.column; i++)
                {
                    //Atribuição para todos os valores ja dividos pelo Pivot
                    prinMatrix[smallNumber, i] = prinMatrix[smallNumber, i] / pivot;

                }
                //-------------------------------------------------------------------------------------



                //Lógica que transforma a coluna escolhida em uma coluna identidade
                for (i = 0; i < Constants.line; i++)
                {
                    if (i != smallNumber)
                    {
                        double multiplyBy = prinMatrix[i, NegativeColumn];
                        if (multiplyBy != 0)
                            ResetLine(prinMatrix, smallNumber, i, multiplyBy * (-1));
                    }
                }


                Console.Write("\n-----------------------------------------------------------------\n");

                //--------------------------Print geral da tabela atual--------------------------
                Console.Write("Interação ");
                Console.Write(interacao);
                Console.Write("\n");

                for (i = 0; i < Constants.line; i++)
                {
                    for (j = 0; j < Constants.column; j++)
                    {
                        Console.Write(prinMatrix[i, j].ToString("F"));
                        Console.Write("  ");
                    }
                    Console.WriteLine("");
                }
                Console.Write("\nValor ótimo = ");
                Console.Write(prinMatrix[Constants.line - 1, Constants.column - 1]);
                for (i = 0; i < Constants.line - 1; i++)
                {
                    Console.Write("\nX");
                    Console.Write(bases[i]);
                    Console.Write(" = ");
                    Console.Write(prinMatrix[i, Constants.column - 1]);
                }
                Console.Write("\n-----------------------------------------------------------------\n");
                //--------------------------Print geral da tabela atual--------------------------

            }
            Console.Write("\n\n");

            Console.Write("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.Write("Resultados Finais!");

            Console.Write("\nValor ótimo Final = ");
            Console.Write(prinMatrix[Constants.line - 1, Constants.column - 1]);
            for (i = 0; i < Constants.line - 1; i++)
            {
                Console.Write("\nX");
                Console.Write(bases[i]);
                Console.Write(" = ");
                Console.Write(prinMatrix[i, Constants.column - 1]);
            }

            Console.Write("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");


            Console.WriteLine("\n\nFim do Simplex!!\n\n");
            Console.Read();
        }



        //Funcao para zerar as linhas
        private static void ResetLine(double[,] x, int lowestBaseIndex, int lineToBeReseted, double multiplyBy)
        {
            for (int i = 0; i < Constants.column; i++)
                x[lineToBeReseted, i] = (x[lowestBaseIndex, i] * multiplyBy) + x[lineToBeReseted, i];
        }
        public static int searchNegative(double[,] matrix)
        {
            int i;

            for (i = 0; i < Constants.column; i++)
            {
                if (matrix[Constants.line - 1, i] < 0)
                {
                    return 0;
                }
            }
            return 1;

        }
        //Função para ler o arquivo e salvar em uma matriz todos os valores
        private static void returnDataTxt(double[,] matrix)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                int readLine = 0, i, j = 0;
                string data;

                //While que irá realizar a leitura e salvamento dos dados dos dados 
                while ((data = sr.ReadLine()) != null)
                {
                    //Split(); =  Divide os dados adquiridos na pesquisa em vetores.
                    string[] aux = data.Split();
                    readLine++;

                    //Caso os dados sejam referente a linha Z
                    if (readLine == 1)
                    {
                        for (i = 0; i < aux.Length; i++)
                        {
                            //Necessário realizar a conversão dos dados inicialmente em String para Double antes da atribuição     
                            matrix[Constants.line - 1, i] = ((Convert.ToDouble(aux[i])) * -1);
                        }
                    }
                    //Caso os dados sejam referente ao restante da tabela
                    else
                    {

                        //Salvamento da qtd de colunas tirando a coluna das restrições
                        Constants.qtdVariable = aux.Length - 1;

                        //Salvamento dos dados na Matrix
                        for (i = 0; i < Constants.column; i++)
                        {

                            //É necessário realizar a conversão de todos dados aux[] 
                            //que inicialmente são String para Double antes da atribuição     

                            if (i < aux.Length - 1)
                            {

                                matrix[readLine - 2, i] = Convert.ToDouble(aux[i]);

                            }
                            //Restrição feita para ignorar os valores da linha Z
                            else if (i == Constants.column - 1)
                            {

                                matrix[readLine - 2, i] = Convert.ToDouble(aux[aux.Length - 1]);

                            }
                        }
                    }
                }//acabou a leitura do arquivo

                Constants.qtdeBases = readLine - 1;
                j = 0;

                for (i = Constants.qtdVariable; i < Constants.qtdVariable + Constants.qtdeBases; i++)
                {
                    matrix[j, i] = 1;
                    j++;
                }
                sr.Close();
            }
        }
        //Função que retorna a Quantidade de linhas detectadas no arquivo txt.
        private static int returnLines()
        {

            string data;
            int cont = 0;
            //Abertura do arquivo .txt
            using (StreamReader sr = File.OpenText(filePath))
            {
                while ((data = sr.ReadLine()) != null)
                {
                    cont++;
                }
                //Fechamento do arquivo .txt
                sr.Close();
            }
            return cont;
        }

        //Função que retorna a Quantidade de linhas detectadas no arquivo txt.
        private static int returnColumns()
        {
            string data;
            string[] aux;

            int countColumn;

            using (StreamReader sr = File.OpenText(filePath))
            {
                data = sr.ReadLine();
                //Split(); =  Divide os dados adquiridos na pesquisa em vetores.
                aux = data.Split();

                sr.Close();
            }

            countColumn = (aux.Length + 1) + (Constants.line - 1);

            return countColumn;
        }
    }
}
