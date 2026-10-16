using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
 
class Student
{
    public int StudentID { get; set; }
    public string? StudentName { get; set; }
    public int Age { get; set; }
    public double MediaVoti { get; set; }
 
    public override string ToString()
    {
        return string.Format($"[StudentID = {StudentID}, StudentName = {StudentName}, Age = {Age}, MediaVoti = {MediaVoti}]");
    }
}
 
class Assenza
{
    public int ID { get; set; }
    public DateTime Giorno { get; set; }
    public int StudentID { get; set; }
}
 
class Persona
{
    public string? Nome { get; set; }
    public int Eta { get; set; }
 
    public override string ToString()
    {
        return string.Format($"Nome : {Nome} , Eta : {Eta}");
    }
}
internal class Program
{
    //stiamo definendo un tipo di puntatore a funzione
    delegate bool CondizioneRicerca(Student s);
 
    public static void AzioneSuElemento(Student s)
    {
        Console.WriteLine(s);
    }
 
    //metodo statico
    public static bool VerificaCondizione(Student s)
    {
        return s.Age >= 18 && s.Age <= 25;
    }
    private static void Main(string[] args)
    {
        //condizione: non devono esistere due studenti con lo stesso StudentID
        //in questo caso si dice che StudentID è chiave primaria della collection
        Student[] studentArray1 =
            {
                new () { StudentID = 1, StudentName = "John", Age = 18 , MediaVoti= 6.5},
                new () { StudentID = 2, StudentName = "Steve",  Age = 21 , MediaVoti= 8},
                new () { StudentID = 3, StudentName = "Bill",  Age = 25, MediaVoti= 7.4},
                new () { StudentID = 4, StudentName = "Ram" , Age = 20, MediaVoti = 10},
                new () { StudentID = 5, StudentName = "Ron" , Age = 31, MediaVoti = 9},
                new () { StudentID = 6, StudentName = "Chris",  Age = 17, MediaVoti = 8.4},
                new () { StudentID = 7, StudentName = "Rob",Age = 19  , MediaVoti = 7.7},
                new () { StudentID = 8, StudentName = "Robert",Age = 22, MediaVoti = 8.1},
                new () { StudentID = 11, StudentName = "John",  Age = 21 , MediaVoti = 8.5},
                new () { StudentID = 12, StudentName = "Bill",  Age = 25, MediaVoti = 7},
                new () { StudentID = 13, StudentName = "Ram" , Age = 20, MediaVoti = 9 },
                new () { StudentID = 14, StudentName = "Ron" , Age = 31, MediaVoti = 9.5},
                new () { StudentID = 15, StudentName = "Chris",  Age = 17, MediaVoti = 8},
                new () { StudentID = 16, StudentName = "Rob2",Age = 19  , MediaVoti = 7},
                new () { StudentID = 17, StudentName = "Robert2",Age = 22, MediaVoti = 8},
                new () { StudentID = 18, StudentName = "Alexander2",Age = 18, MediaVoti = 9},
                };
 
        Student[] studentResultArray;
        List<Student> studentResultList;
 
        //definiamo delle condizioni di ricerca
        //primo modo: uso di Func con lambda
        Func<Student, bool> condizioneDiRicerca = s => s.Age >= 18 && s.Age <= 25;
        //secondo modo: uso di un delegato implementato attraverso lambda
        CondizioneRicerca condizioneDiRicerca2 = s => s.Age >= 18 && s.Age <= 25;
        //terzo modo: uso di un delegato che punta a un metodo precedentemente definito
        CondizioneRicerca condizioneDiRicerca3 = VerificaCondizione;
        //quarto modo: usiamo direttamente la lambda - il più comodo
 
        // vecchio metodo per copiare gli elementi di un array in una lista
        List<Student> listaStudent = [];
        // ciclo su array per copieare gli elementi nella lista
        foreach (var item in studentArray1)
        {
            listaStudent.Add(item);
        }
        // // altro modo alternativo
        // listaStudent.AddRange(studentArray1);
 
        //creiamo una lista con gli stessi oggetti presenti nell'array
        // listaStudent = studentArray1.ToList();
        // listaStudent = [..studentArray1];
        List<Student> studentList1 = [.. studentArray1];//equivalente a invocare studentArray1.ToList();
                                                        // List<Student> listaStudenti = studentArray1.ToList();
                                                        // List<Student> listaStudenti2 = [..studentArray1];
 
        // trovare tutti gli studenti che hanno età compresa tra 18 e 25 anni
        studentResultArray = studentArray1.Where(s => s.Age >= 18 && s.Age <= 25).ToArray();
        foreach (var studente in studentResultArray)
        {
            Console.WriteLine(studente);
        }
 
        // stessa query su una lista
        List<Student> lista = [.. studentList1.Where(s => s.Age >= 18 && s.Age <= 25)];
        studentResultList = studentList1.Where(s => s.Age >= 18 && s.Age <= 25).ToList();
        Console.WriteLine("Caso della lista");
        foreach (var studente in studentResultList)
        {
            Console.WriteLine(studente);
        }
        //Use LINQ to find first student whose name is Bill
        Student? firstBill = studentList1.FirstOrDefault(s => s.StudentName == "Bill");
        if(firstBill is not null)
        {
            Console.WriteLine("\nPrimo studente che si chiama Bill");
            Console.WriteLine(firstBill);
        }
        //trovare lo studente con un dato ID
        var studentConID = studentList1.FirstOrDefault(s => s.StudentID == 5);
 
        //Stampa media voti
        Console.WriteLine("\nStampa età e  media voti");
        foreach (var studente in studentList1)
        {
            Console.WriteLine($"{studente.StudentID} {studente.Age} {studente.MediaVoti}");
        }
        //versione LINQ
        //il ForEach permette di eseguire una Action su ognu elemento della collection
        studentList1.ForEach(s =>
        {
            Console.WriteLine($"{s.StudentID} {s.Age} {s.MediaVoti}");
        });
        //esistono delle veresioni con indice dei metodi del LINQ
        //stampare gli studenti che sono teenager e che hanno indice pari nella listaStudent
        var studentiConIndicePari = studentList1
        .Where((s,i) => s.Age > 18 && s.Age < 25 && i % 2 == 0)
        .ToList();
        //stampa
        Console.WriteLine("Studenti teenager con indice pari stampa con foreach");
        studentiConIndicePari.ForEach(Console.WriteLine);
        System.Console.WriteLine("stampa secondo metodo:");
        //se devo solo stampare e non ho bisogno di altre laborazioni
        studentList1
        .Where((s,i) => s.Age > 18 && s.Age < 25 && i % 2 == 0)
        .ToList().ForEach(st => Console.WriteLine($"{st.StudentID} {st.StudentName} {st.Age} {st.MediaVoti}"));
 
        //ESEMPIO OFTYPE
        IList mixedList = new ArrayList
            {
                5,
                "numero uno",
                true,
                "numero due",
                new Student() { StudentID = 10, Age = 30, StudentName = "Roberto" }
            };
 
        //filtro elementi di tipo studenti
        System.Console.WriteLine("Sampa di studenti in una lista con ofTYPE:");
        var studentiInMixed= mixedList
                                .OfType<Student>()
                                .ToList();
        studentiInMixed.ForEach(Console.WriteLine);
 
        //ordinamento di una collection mediante ORDERBY
        //var listaStudentiOrdinataPerEta = studentList1.OrderBy(s => s.Age).ToList();
        System.Console.WriteLine("Stampa studenti ordinati per età");
        //listaStudentiOrdinataPerEta.ForEach(Console.WriteLine);
 
        //se devo solo stampare
        studentList1.OrderBy(s => s.Age)
            .ToList()
            .ForEach(Console.WriteLine);
        System.Console.WriteLine("Stampa studenti ordinati per media voti");
        studentList1.OrderBy(s => s.MediaVoti)
            .ToList()
            .ForEach(Console.WriteLine);
        System.Console.WriteLine("Stampa studenti ordinati prima per eta poi per mediavoti");
        //ordino studenti prima per eta poi in base a media voti
        studentList1
            .OrderBy(s => s.Age)
            .ThenBy(s => s.MediaVoti)
            .ToList()
            .ForEach(Console.WriteLine);
 
        //per ordinamento decrescente
        System.Console.WriteLine("Stampa studenti ordinati per media voti DECRESCENTI");
        studentList1
            .OrderByDescending(s => s.MediaVoti)
            .ToList()
            .ForEach(Console.WriteLine);
            //ThenbyDescending
            //se ci deve essere un terzo ordinamento devo:
 
        studentList1
            .OrderByDescending(s => s.MediaVoti)
            .ThenBy(s => s.Age)
            .OrderBy(s => s.StudentName)
            .ToList()
            .ForEach(Console.WriteLine);
 
        //studio della clausola Select
        //proietto una collection di student in una collection di oggetti anonimi
        var listaAnonima = studentList1.Select(s => new {Nome = s.StudentName, Eta = s.Age}).ToList();
        Console.WriteLine("Stampa studenti con Select in oggetti anonimi");
        listaAnonima.ForEach(Console.WriteLine);
        Console.WriteLine("Stampa studenti con Select in oggetti anonimi SOLO NOME");
        listaAnonima.ForEach(s => Console.WriteLine($"Nome: {s.Nome}"));
        //caso di select con tipi non anonimi
        var listaPersone = studentList1.Select(s => new Persona()
        {
            Nome = s.StudentName,
            Eta = s.Age
        }).ToList();
        listaPersone.ForEach(System.Console.WriteLine);
        //esempi di raggruppameento - GroupBy
        //ragruppare gli studenti per età
        System.Console.WriteLine("ragruppamento studenti per eta");
        var groupedResult = studentList1.GroupBy(s => s.Age);
        foreach (var group in groupedResult)
        {
            System.Console.WriteLine($"gruppo di studenti con età = {group.Key}");
            System.Console.WriteLine($"Media voti = {group.Average(s => s.MediaVoti)}");
            System.Console.WriteLine($"voto minimo = {group.Min(s => s.MediaVoti)}");
            System.Console.WriteLine($"voto minimo = {group.Max(s => s.MediaVoti)}");
            foreach (var studente in group)
            {
                System.Console.WriteLine(studente);
            }
        }
       
    }
}