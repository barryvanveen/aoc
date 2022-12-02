namespace aoc2022day2;

public struct RockPaperScissorsLine
{
    private readonly Shape _opponent;
    private readonly Shape _you;

    public RockPaperScissorsLine(string opponent, string you)
    {
        switch (opponent)
        {
            case "A":
                _opponent = Shape.Rock;
                break;
            case "B":
                _opponent = Shape.Paper;
                break;
            case "C":
                _opponent = Shape.Scissors;
                break;
            default:
                throw new Exception($"Unknown opponent shape {opponent}");
        }
        
        switch (you)
        {
            case "X":
                _you = Shape.Rock;
                break;
            case "Y":
                _you = Shape.Paper;
                break;
            case "Z":
                _you = Shape.Scissors;
                break;
            default:
                throw new Exception($"Unknown you shape {you}");
        }
    }

    public RockPaperScissorsLine(string opponent, Shape you)
    {
        switch (opponent)
        {
            case "A":
                _opponent = Shape.Rock;
                break;
            case "B":
                _opponent = Shape.Paper;
                break;
            case "C":
                _opponent = Shape.Scissors;
                break;
            default:
                throw new Exception($"Unknown opponent shape {opponent}");
        }

        _you = you;
    }

    public int Score()
    {
        int shapeScore = ShapeScore();
        Console.WriteLine($"Shape score of {_you} is {shapeScore}");
        int outcomeScore = OutcomeScore(); 
        Console.WriteLine($"Outcome score of {_opponent} {_you} is {outcomeScore}");
        return shapeScore + outcomeScore;
    }

    private int ShapeScore()
    {
        switch (_you)
        {
            case Shape.Rock:
                return 1;
            case Shape.Paper:
                return 2;
            case Shape.Scissors:
                return 3;
            default:
                throw new Exception($"Unknown score for {_you}");
        }
    }
    
    private int OutcomeScore()
    {
        if (_opponent == _you)
        {
            return 3;
        }

        if (
            (_opponent == Shape.Rock && _you == Shape.Scissors) || 
            (_opponent == Shape.Paper && _you == Shape.Rock) ||
            (_opponent == Shape.Scissors && _you == Shape.Paper)
        )
        {
            return 0;
        }

        return 6;
    }
}