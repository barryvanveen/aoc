// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);
		
		int rows = lines[0].Length;
		int cols = lines.Length;
		int[,] map = new int[rows,cols];
		int[,] visible = new int[rows,cols];
		int[,] scores = new int[rows,cols];
		int row, col, max;

		row = 0;
		foreach (string line in lines)
		{
			col = 0;
			foreach (char tree in line) 
			{
				map[row, col] = Convert.ToInt32(tree) - '0';
				visible[row, col] = 0;
				col++;				
			}
			row++;
		}

		Console.WriteLine("Map:");
		for (row = 0; row<rows; row++) {
			for (col = 0; col<cols; col++) {
				Console.Write(map[row, col]);
			}
			Console.Write("\n");
		}

		// from Left
		for (row = 0; row<rows; row++) {
			max = map[row, 0];
			visible[row, 0] = 1;
			for (col = 1; col<cols; col++) {
				if (map[row, col] <= max) {
					continue;
				}

				max = map[row, col];
				visible[row, col] = 1;
			}
		}

		// from Top
		for (col = 0; col<cols; col++) {
			max = map[0, col];
			visible[0, col] = 1;
			for (row = 1; row<rows; row++) {
				if (map[row, col] <= max) {
					continue;
				}

				max = map[row, col];
				visible[row, col] = 1;
			}
		}

		// from Right
		for (row = 0; row<rows; row++) {
			max = map[row, cols-1];
			visible[row, cols-1] = 1;
			for (col = cols-2; col>=0; col--) {
				if (map[row, col] <= max) {
					continue;
				}

				max = map[row, col];
				visible[row, col] = 1;
			}
		}

		// from Bottom
		for (col = 0; col<cols; col++) {
			max = map[rows-1, col];
			visible[rows-1, col] = 1;
			for (row = rows-2; row>=0; row--) {
				if (map[row, col] <= max) {
					continue;
				}

				max = map[row, col];
				visible[row, col] = 1;
			}
		}

		Console.WriteLine("Visible:");
		for (row = 0; row<rows; row++) {
			for (col = 0; col<cols; col++) {
				Console.Write(visible[row, col]);
			}
			Console.Write("\n");
		}
		
		var sum = visible.Cast<int>().Sum();
		
		Console.WriteLine($"Answer #1 is {sum}");

		for (row = 0; row < rows; row++)
		{
			for (col = 0; col < cols; col++)
			{
				scores[row, col] = ScenicScore(row, col, map);
			}
		}
		
		Console.WriteLine("Scores:");
		for (row = 0; row<rows; row++) {
			for (col = 0; col<cols; col++) {
				Console.Write(scores[row, col]);
			}
			Console.Write("\n");
		}
		
		max = scores.Cast<int>().Max();
		
		Console.WriteLine($"Answer #2 is {max}");
    }

    private static int ScenicScore(int myRow, int myCol, int[,] map)
    {
	    List<int> visibilities = new List<int>();
	    int row, col, sum;
	    int myHeight = map[myRow, myCol];
	    int maxRow = map.GetLength(0);
	    int maxCol = map.GetLength(1);

	    if (myRow == 0 || myRow == maxRow - 1 || myCol == 0 || myCol == maxCol - 1)
	    {
		    return 0;
	    }
	    
	    // look up
	    sum = 0;
	    for (row = myRow-1; row >= 0; row--)
	    {
		    sum++;

		    if (row == 0 || map[row, myCol] >= myHeight)
		    {
			    visibilities.Add(sum);
			    break;
		    } 
	    }
	    
	    // look left
	    sum = 0;
	    for (col = myCol-1; col >= 0; col--)
	    {
		    sum++;

		    if (col == 0 || map[myRow, col] >= myHeight)
		    {
			    visibilities.Add(sum);
			    break;
		    } 
	    }
	    
	    // look down
	    sum = 0;
	    for (row = myRow+1; row < maxRow; row++)
	    {
		    sum++;

		    if (row == maxRow-1 || map[row, myCol] >= myHeight)
		    {
			    visibilities.Add(sum);
			    break;
		    } 
	    }
	    
	    // look right
	    sum = 0;
	    for (col = myCol+1; col < maxCol; col++)
	    {
		    sum++;

		    if (col == maxCol-1 || map[myRow, col] >= myHeight)
		    {
			    visibilities.Add(sum);
			    break;
		    } 
	    }

	    foreach (int x in visibilities)
	    {
		    Console.Write($"{x} ");
	    }
	    Console.Write("\n");
		    
		sum = visibilities.Aggregate((total, next) => total * next);
		Console.WriteLine($"Scenic score is {sum}");

		return sum;
    }
}