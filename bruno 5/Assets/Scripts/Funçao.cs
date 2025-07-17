using UnityEngine;

public class Funçao : MonoBehaviour
{
    public int n;
    void Start()
    {
        Repetir(n);
        // Repetir2(n);
    }
    void Repetir(int n)
    {
        for (int i = 0; i < n; i++)
        {
            string resultado = "";
            for (int j = 0; j < i + 1; j++)
            {
                resultado += i + 1 + " ";
            }
            print(resultado);

        }
    }
}