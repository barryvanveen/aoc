namespace aoc2022day2;

public struct RockPaperScissorsShapeDecider
{
    private readonly Shape _opponent;
    private readonly Outcome _outcome;

    public RockPaperScissorsShapeDecider(string opponent, string outcome)
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
        
        switch (outcome)
        {
            case "X":
                _outcome = Outcome.Lose;
                break;
            case "Y":
                _outcome = Outcome.Draw;
                break;
            case "Z":
                _outcome = Outcome.Win;
                break;
            default:
                throw new Exception($"Unknown outcome {outcome}");
        }
    }

    public Shape Decide()
    {
        Shape decision;
        
        switch (_outcome)
        {
            case Outcome.Lose:
                decision = Lose(_opponent);
                Console.WriteLine($"Lose from {_opponent} results in {decision}");
                return decision;
            case Outcome.Draw:
                decision = _opponent;
                Console.WriteLine($"Draw with {_opponent} results in {decision}");
                return decision;
            case Outcome.Win:
                decision = Win(_opponent);
                Console.WriteLine($"Win from {_opponent} results in {decision}");
                return decision;
            default:
                throw new Exception($"Cannot decide for {_opponent} and {_outcome}");
        }
    }

    private Shape Win(Shape opponent)
    {
        switch (opponent)
        {
            case Shape.Rock:
                return Shape.Paper;
            case Shape.Paper:
                return Shape.Scissors;
            case Shape.Scissors:
                return Shape.Rock;
            default:
                throw new Exception($"Cannot win for {_opponent}");
        }
    }
    
    private Shape Lose(Shape opponent)
    {
        switch (opponent)
        {
            case Shape.Rock:
                return Shape.Scissors;
            case Shape.Paper:
                return Shape.Rock;
            case Shape.Scissors:
                return Shape.Paper;
            default:
                throw new Exception($"Cannot lose for {_opponent}");
        }
    }
}