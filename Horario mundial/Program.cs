using System;
using System.Collections.Generic;
using System.Threading;

public interface IObservador
{
    void Actualizar(DateTime hora);
}


public class Reloj
{
    private DateTime horaActual;
    private List<IObservador> observadores = new List<IObservador>();

    public void AgregarObservador(IObservador observador)
    {
        observadores.Add(observador);
    }

    public void QuitarObservador(IObservador observador)
    {
        observadores.Remove(observador);
    }

    public void PonEnHora()
    {
        horaActual = DateTime.Now;
        NotificarObservadores();
    }

    private void NotificarObservadores()
    {
        foreach (var observador in observadores)
        {
            observador.Actualizar(horaActual);
        }
    }
}



public class HoraEn : IObservador
{
    private string ciudad;
    private int diferenciaHoraria;

    public HoraEn(string ciudad, int diferenciaHoraria)
    {
        if (diferenciaHoraria < -12 || diferenciaHoraria > 12)
        {
            throw new ArgumentException("La diferencia es");
        }

        this.ciudad = ciudad;
        this.diferenciaHoraria = diferenciaHoraria;
    }

    public void Actualizar(DateTime horaBase)
    {
        DateTime horaLocal = horaBase.AddHours(diferenciaHoraria);
        
        Console.WriteLine($"{ciudad}, {horaLocal:HH:mm:ss}");
    }
}

public class Aplicacion
{
    public static void Main()
    {
        Reloj reloj = new Reloj();

        HoraEn madrid = new HoraEn("Madrid", 1);
        HoraEn nuevaYork = new HoraEn("Nueva York", -5);
        HoraEn tokio = new HoraEn("Tokio", 9);
        HoraEn sidney = new HoraEn("Sidney", 10);

        reloj.AgregarObservador(madrid);
        reloj.AgregarObservador(nuevaYork);
        reloj.AgregarObservador(tokio);
        reloj.AgregarObservador(sidney);

        for (int i = 0; i < 10; i++)
        {
            reloj.PonEnHora();
            Thread.Sleep(1000); 
        }
    }
}

//No nos repruebe maestra, a pura documentación y recuerdos de Canto pudimos llegar hasta acá
//PD: Larga vida a PYTHON 