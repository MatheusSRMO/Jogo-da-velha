using System;
using System.Collections.Generic;

namespace Jogo_da_velha {
    class Program {
        static void Main(string[] args) {
            while (true) {
                Console.Write("Deseja iniciar?");
                var opn2 = Console.ReadLine();
                if (opn2 == "1") {
                    Dictionary<string, string> j1 = new Dictionary<string, string>();
                    Dictionary<string, string> j2 = new Dictionary<string, string>();


                    Console.WriteLine("Bem vindo ao jogo da velha!");
                    //Nome do jogador -> forma que ele quer
                    Console.Write("Digite o nome do primeiro player: ");
                    var nome_j1 = Console.ReadLine();
                    j1.Add("Nome", nome_j1);
                    Console.WriteLine("Digite a forma \n1 - O\n2 - X");
                    string[] opções = new string[2] { "O", "X" };

                    int opção;
                    do {
                        Console.Write("Digite a sua escolha: ");
                        opção = int.Parse(Console.ReadLine());
                    } while (opção != 1 && opção != 2);
                    j1.Add("Forma", opções[opção - 1]);


                    Console.Write("Digite o nome do segundo player: ");
                    var nome_j2 = Console.ReadLine();
                    j2.Add("Nome", nome_j2);
                    if (opção == 1) j2.Add("Forma", opções[1]); else j2.Add("Forma", opções[0]);

                    Console.WriteLine("\n========================================");
                    Console.WriteLine("{0} ficou com a forma {1}", j1["Nome"], j1["Forma"]);
                    Console.WriteLine("{0} ficou com a forma {1}", j2["Nome"], j2["Forma"]);
                    Console.WriteLine("========================================\n");

                    Console.WriteLine("Precione ENTER para iniciar");
                    Console.ReadLine();

                    var matriz = new string[3, 3];
                    for (int linha = 0; linha < 3; linha++) {
                        for (int coluna = 0; coluna < 3; coluna++) {
                            matriz[linha, coluna] = " ";
                        }
                    }
                    var cont = 0;
                    while (cont < 9) {
                        Console.Clear();
                        mostra(matriz);
                        if (cont % 2 == 0) {
                            string[] opn;
                            do {
                                Console.WriteLine("É a vez do jogador {0}", j1["Nome"]);
                                Console.Write("Digite a linha e a coluna: ");
                                opn = (Console.ReadLine()).Split(' ');
                            } while (opn.Length != 2 || (int.Parse(opn[0]) > 3) || (int.Parse(opn[0]) < 1) || (int.Parse(opn[1]) > 3) || (int.Parse(opn[1]) < 1) || matriz[int.Parse(opn[0]) - 1, int.Parse(opn[1]) - 1] != " ");
                            matriz[int.Parse(opn[0]) - 1, int.Parse(opn[1]) - 1] = j1["Forma"];
                            var finish = verifica(j1, opções, matriz);
                            if (finish) break;
                        }
                        else {
                            string[] opn;
                            do {
                                Console.WriteLine("É a vez do jogador {0}", j2["Nome"]);
                                Console.Write("Digite a linha e a coluna: ");
                                opn = (Console.ReadLine()).Split(' ');
                            } while (opn.Length != 2 || matriz[int.Parse(opn[0]) - 1, int.Parse(opn[1]) - 1] != " ");
                            matriz[int.Parse(opn[0]) - 1, int.Parse(opn[1]) - 1] = j2["Forma"];
                            var finish = verifica(j2, opções, matriz);
                            if (finish) break;
                        }
                        cont += 1;
                    }
                    if (cont >= 9) {
                        Console.Clear();
                        Console.WriteLine("\n==================================");
                        Console.WriteLine("             Empate!");
                        Console.WriteLine("==================================\n");
                    }
                    mostra(matriz);
                    Console.ReadLine();
                }
                
                else {
                    break;
                }
            }
        }
        static void mostra(string[,] matriz) {
            Console.WriteLine("\n============");
            Console.WriteLine("  1  2  3");
            for (int linha = 0; linha < 3; linha++) {
                Console.Write(linha + 1);
                for (int coluna = 0; coluna < 3; coluna++) {
                    Console.Write(" {0} ", matriz[linha, coluna]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("============\n");
        }
        static void winner(Dictionary<string, string> jogador) {
            Console.Clear();
            Console.WriteLine("\n==================================");
            Console.WriteLine("O jogador {0} Venceu", jogador["Nome"]);
            Console.WriteLine("==================================\n");
        }
        static bool verifica(Dictionary<string,string> jogador ,string[] opções, string[,] matriz) {
            var forma = jogador["Forma"];
            for (int linha = 0; linha < 3; linha++) {
                var ver_linha = 0;
                var ver_coluna = 0;
                var ver_diagonal_principal = 0;
                var ver_diagonal_secundaria = 0;
                for (int coluna = 0; coluna < 3; coluna++) {
                    //Verificar nas linhas
                    if (forma == matriz[linha, coluna]) {
                        ver_linha += 1;
                        if (ver_linha == 3) {
                            winner(jogador);
                            return true;
                        }
                    }
                    //Verificar nas colunas
                    if (forma == matriz[coluna, linha]) {
                        ver_coluna += 1;
                        if (ver_coluna == 3) {
                            winner(jogador);
                            return true;
                        }
                    }

                    //Verificar na diagonal Principal
                    if (forma == matriz[coluna, coluna]) {
                        ver_diagonal_principal += 1;
                        if (ver_diagonal_principal == 3) {
                            winner(jogador);
                            return true;
                        }
                    }
                    //Verificar na diagonal Secundaria
                    if (forma == matriz[0, 2] && forma == matriz[1,1] && forma == matriz[2,0]) {
                        ver_diagonal_secundaria += 1;
                        if (ver_diagonal_secundaria == 3) {
                            winner(jogador);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
