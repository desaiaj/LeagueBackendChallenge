using System.Numerics;

namespace League.BackendChallenge;

public class LeagueBackendChallenge
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter filename or absolute path to the file: ");
            string? filePath = Console.ReadLine();
            if (string.IsNullOrEmpty(filePath))
                throw new Exception("No filename provided, expects file with Matrix");

            var challenge = new LeagueBackendChallenge();

            string[] fileData = challenge.ReadFile_Filtered_SpaceOrEmpty(filePath);

            List<List<int>> matrix = new List<List<int>>();
            challenge.PrepareMatrix(fileData, matrix);

            //print matrix fetched from file
            Console.WriteLine("Provided Matrix");
            matrix.ForEach(x => Console.WriteLine(string.Join(',', x)));

            //Invert Matrix
            Console.WriteLine("\nInvert Matrix");
            var invertedMatrix = challenge.InvertMatrix(matrix);
            invertedMatrix.ForEach(x => Console.WriteLine(string.Join(',', x)));

            //Print Flatten Matrix
            Console.WriteLine("\nPrint Flatten Matrix");
            Console.WriteLine(challenge.FlattenMatrix(matrix));

            //Sum Matrix
            Console.WriteLine("\nSum of Matrix");
            Console.WriteLine(challenge.SumOfMatrix(matrix));

            //Product of Matrix
            Console.WriteLine("\nProduct of Matrix");
            Console.WriteLine(challenge.ProductOfMatrix(matrix));

            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Operation canceled: " + e.Message);
        }
    }

    /// <summary>
    /// prepare Matrix
    /// </summary>
    /// <param name="fileData"></param>
    /// <param name="matrix"></param>
    public void PrepareMatrix(string[] fileData, List<List<int>> matrix)
    {
        try
        {
            for (int i = 0; i < fileData.Length; i++)
            {
                List<int> lines = fileData[i].Split(',').Select(int.Parse).ToList();
                matrix.Insert(i, lines);
                if (matrix[i].Count() != fileData.Length)
                {
                    throw new InvalidDataException();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Provided Matrix is invalid, expects same number of Row N x Col M of Integers only");
        }
    }

    /// <summary>
    /// Function to read text from file filtered null or empty lines
    /// </summary>
    /// <param name="path"></param>
    /// <returns>string[]</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public string[] ReadFile_Filtered_SpaceOrEmpty(string path)
    {
        try
        {
            string[] lines;
            string filePath = Path.GetFullPath(path);
            if (File.Exists(filePath))
            {
                // Store each line in array of strings 
                lines = File.ReadAllLines(filePath);
                if (lines.Length < 0)
                {
                    throw new Exception("File is empty");
                }

                lines.Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x));
            }
            else
                throw new FileNotFoundException("File not found");

            return lines;
        }
        catch (FileNotFoundException)
        {
            throw;
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("Provided file path is not valid");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message} : {ex.StackTrace}");
        }
    }

    /// <summary>
    /// Function to invert the given matrix
    /// </summary>
    /// <param name="matrix"></param>
    public List<List<int>> InvertMatrix(List<List<int>> matrix)
    {
        ValidateMatrix(matrix);

        List<List<int>> invertedMatrix = new List<List<int>>();
        for (int i = 0; i < matrix.Count; i++)
        {
            //allocate initial memory for the list
            invertedMatrix.Insert(i, new List<int>());
            for (int j = 0; j < matrix[i].Count; j++)
            {
                int num = matrix[j][i];
                //add matrix element to the list
                invertedMatrix[i].Insert(j, num);
            }
        }
        return invertedMatrix;
    }

    /// <summary>
    /// Function to print the Flatten matrix 
    /// </summary>
    /// <param name="matrix"></param>
    public string FlattenMatrix(List<List<int>> matrix)
    {
        ValidateMatrix(matrix);

        string flattenMatrix = "";
        for (int i = 0; i < matrix.Count; i++)
        {
            flattenMatrix += string.Join(',', matrix[i]);
            flattenMatrix += i != matrix.Count - 1 ? ',' : null;
        }
        return flattenMatrix;
    }

    /// <summary>
    /// Calculate sum of all elements of the given matrix
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public int SumOfMatrix(List<List<int>> matrix)
    {
        ValidateMatrix(matrix);

        int sum;
        sum = matrix.Select(x => x.Sum(y => y)).Sum();
        return sum;
    }

    /// <summary>
    /// Calculate Product of all elements of the given matrix
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public BigInteger ProductOfMatrix(List<List<int>> matrix)
    {
        ValidateMatrix(matrix);

        BigInteger product = 1;
        matrix.ForEach(x => x.ForEach(y => product *= y));
        return product;
    }

    /// <summary>
    /// Function to validate the given matrix
    /// </summary>
    /// <param name="matrix"></param>
    /// <exception cref="ArgumentException"></exception>
    public void ValidateMatrix(List<List<int>> matrix)
    {
        if (matrix is null || matrix.Count <= 0)
        {
            throw new ArgumentException("Provided matrix is invalid, please check the input");
        }
    }
}