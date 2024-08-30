Console.WriteLine("¡¡Bienvenido al renovado juego de Pokemon!!");
Pokemon[] pokemons = new Pokemon[2];

for (int i = 1; i <= 2; i++)
{
    Console.WriteLine($"\nPokemon {i}");
    Console.WriteLine("Ingrese el nombre del Pokemon: ");
    string name = Console.ReadLine()!;
    Console.WriteLine("Ingrese el tipo del Pokemon: ");
    string type = Console.ReadLine()!;
    bool defenseOk = false;
    int defenseValue = 0;
    while (!defenseOk)
    {
        Console.WriteLine("Ingrese el valor de la defensa (debe ser un valor entre 10 y 35): ");
        defenseValue = int.Parse(Console.ReadLine()!);
        if (defenseValue >= 10 && defenseValue <= 35)
        {
            defenseOk = true;
        }
    }

    List<Ataque> attacks = new List<Ataque>();
    for (int j = 1; j <= 3; j++)
    {
        Console.WriteLine($"Ingrese el nombre del ataque {j}: ");
        string attackName = Console.ReadLine()!;
        bool attackOk = false;
        int attackValue = 0;
        while (!attackOk)
        {
            Console.WriteLine($"Ingrese el valor del ataque {j} (debe ser un valor entre 0 y 40): ");
            attackValue = int.Parse(Console.ReadLine()!);
            if (attackValue >= 0 && attackValue <= 40)
            {
                attackOk = true;
            }
        }

        attacks.Add(new Ataque(attackName, attackValue));
    }

    pokemons[i - 1] = new Pokemon(attacks, defenseValue, name, type);

}

IBattle battle = new Battle(pokemons);

battle.StartBattle();


public interface IHabilidades
{
    int atacar();
    double defender();

}


public class Pokemon : AHabilidades
{
    public Pokemon(List<Ataque> ataques, int defensa, string nombre, string tipo) : base(ataques, defensa)
    {
        this.nombre = nombre;
        this.tipo = tipo;
    }
    private string nombre { get; set; }
    private string tipo { get; set; }

    public string getNombre()
    {
        return nombre;
    }

    public string getTipo()
    {
        return tipo;
    }

}


public class Ataque
{
    public Ataque(string nombre, int valor)
    {
        this.nombre = nombre;
        this.valor = valor;
    }
    private string nombre { get; set; }
    private int valor { get; set; }

    public string getNombre()
    {
        return nombre;
    }

    public int getValor()
    {
        return valor;
    }

}


public abstract class AHabilidades : IHabilidades
{
    public AHabilidades(List<Ataque> ataques, int defensa)
    {
        this.ataques = ataques;
        this.defensa = defensa;
    }
    private int defensa { get; set; }
    private List<Ataque> ataques { get; set; }

    public int atacar()
    {
        Random random = new Random();

        int index = random.Next(0, ataques.Count);

        Ataque attack = ataques[index];

        int x = random.Next(0, 2);

        int result = attack.getValor() * x;

        return result;
    }

    public double defender()
    {
        Random random = new Random();

        double a = 1;
        double b = 0.5;

        int opt = random.Next(0, 2);
        double result = 0;

        if (opt == 0)
        {
            result = defensa * a;
        }
        else if (opt == 1)
        {
            result = defensa * b;
        }

        return result;
    }
}


public interface IBattle
{
    void StartBattle();
}


public class Battle : IBattle
{
    public Battle(Pokemon[] pokemons)
    {
        this.pokemons = pokemons;
    }
    private Pokemon[] pokemons {get; set; }
    public void StartBattle()
    {
        Random random = new Random();
        double acum1 = 0;
        double acum2 = 0;


        for (int k = 0; k < 3; k++)
        {
            double[] result = new double[2];
            bool[] action = new bool[2];

            Console.WriteLine($"Ronda {k + 1}");

            for (int i = 0; i <= 1; i++)
            {
                int opt = random.Next(0, 2);
                if (opt == 0)
                {
                    result[i] = pokemons[i].atacar();
                    action[i] = true;
                    Console.WriteLine($"El Pokemon {i + 1} ha atacado.");
                }
                else if (opt == 1)
                {
                    result[i] = pokemons[i].defender();
                    action[i] = false;
                    Console.WriteLine($"El Pokemon {i + 1} ha defendido.");
                }
            }

            //si los dos defienden, no se suman puntos a nadie.
            if (action[0] == false && action[1] == false)
            {
                Console.WriteLine($"Los dos Pokemons han defendido.");
                continue;
            }

            double res = result[0] - result[1];

            if (res > 0)
            {
                acum1 += res;
            }
            else if (res < 0)
            {
                acum2 += res;
            }

        }

        if (acum1 > acum2)
        {
            Console.WriteLine($"El ganador es el Pokemon 1, llamado {pokemons[0].getNombre()} de tipo {pokemons[0].getTipo()} con {acum1} puntos a favor.");
        }
        else if (acum1 < acum2)
        {
            Console.WriteLine($"El ganador es el Pokemon 2, llamado {pokemons[1].getNombre()} de tipo {pokemons[1].getTipo()} con {acum2} puntos a favor.");
        }
        else
        {
            Console.WriteLine($"Hay un empate.");
        }

    }
}