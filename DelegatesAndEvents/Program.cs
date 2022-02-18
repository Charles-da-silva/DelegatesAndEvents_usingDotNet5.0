using System;

namespace DelegatesAndEvents
{
    
    // Imagine que existe um Delegate "OperacoesMatematicas" e após criar uma variavel chamada "calculos" você atribui os métodos Somar(), Subtratir(), Multiplicar() e 
    // Dividir(). Uma vez executada a variável "calculos" todos os métodos serão executados. 
    // Agora pense que você gostaria que na executação de cada método você gostaria que outras duas ações foram executadas (envio de email e registro em bando de dados). 
    // Pode-se criar um delegate "Acoes", criar uma variável "actions" e vincular os dois métodos (email e banco) nesta variável, para então executada. 
    // Detalhe que isso tudo deve ser executado dentro de cada método (Somar(), Subtratir(), Multiplicar() e Dividir()).

    // É muito mais fácil, usa menos memória e deixa o programa mais performático se usar um Event. Ou seja, Você cria um Delegate "DelegateToEvent", 
    // um Event "MyEvent" e vincula (assina) este evento uma única vez com os métodos desejados (email e banco). Quando desejar ser notificado, 
    // em vez de criar uma variável de um delegate, vincular métodos e executar, basta executar o "MyEvent.Invoke()", ou seja, basta adicionar o "MyEvent.Invoke()" 
    // dentro dos métodos que estão dentro do Delegate original. 

    // ABAIXO SEGUE UM EXEMPLO ONDE NO MÉTODO SOMAR EU USEI O INVOKE DO EVENT, AGORA NO MÉTODO SUBTRAIR FOI PRECISO CRIAR UMA VARIÁVEL DE UM DELEGATE E DEPOIS EXECUTÁ-LA, 
    // POIS O "DELEGATE.INVOKE" NÃO PODE SER USADO. IMAGINE SE TIVER QUE FAZER ISSO PARA UNS 15 MÉTODOS DIFERENTES. O EVENT.INVOKE SE TORNA MUITO MELHOR.
    
    class Program
    {
        public delegate double DelegateSimples (double x, double y);
        public delegate void DelegateToEvent (double r);
        public static event DelegateToEvent MyEvent;

        public static double Somar (double x, double y)
            {
                double r = x + y;
                MyEvent.Invoke(r);
                return r; 
            }

        public static double Subtrair (double x, double y)
            {
                double r = x - y;
                DelegateToEvent registroBanco = new DelegateToEvent(RegistrarNoBanco);
                registroBanco.Invoke(r);                
                return r; 
            }

        static void Main(string[] args)
        {
            MyEvent += MostrarResultadoNaTela;
            MyEvent += EnviarEmail;
            DelegateSimples op = new DelegateSimples(Somar); 
            op += Subtrair;
            op(10, 5);                
        }

       public static void MostrarResultadoNaTela(double r)
       {
           System.Console.WriteLine($"Resultado: {r}");
       }

       public static void EnviarEmail(double r)
       {
           System.Console.WriteLine("E-mail enviado com sucesso");
       }

       public static void RegistrarNoBanco(double r)
       {
           System.Console.WriteLine("Dado registrado no banco");
       }
    }
}
