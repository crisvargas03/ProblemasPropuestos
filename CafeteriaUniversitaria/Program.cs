class Program
{
    private const int maxCapacity = 3;
    private static SemaphoreSlim semaphore = new(maxCapacity);

    static async Task Main()
    {
        List<Task> studentTasks = [];
        for (int i = 1; i <= 20; i++)
        {
            studentTasks.Add(EnterToCafeteria(i));
        }
        await Task.WhenAll(studentTasks);
        Console.WriteLine("Todos los estudiantes han sido atendidos. Cerrando la cafetería.");
    }

    static async Task EnterToCafeteria(int studentId)
    {
        Console.WriteLine($"Estudiante {studentId} espera en la fila para entrar a la cafetería.");

        await semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"Estudiante {studentId} entra a la cafetería y realiza su compra.");
            await Task.Delay(2000);
        }
        finally
        {
            Console.WriteLine($"Estudiante {studentId} sale de la cafetería.");
            semaphore.Release();
        }
    }
}