using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4Problem2
{
    public class Prize
    {
        public double Value { get; private set; }

        public Prize(double value)
        {
            Value = value;
        }

        public void UpdateValue(double addition)
        {
            Value += addition;
        }
    }

    public class PrizeCollection
    {
        public LinkedList<Prize> Prizes { get; private set; }

        public int TotalPrizeCount { get; private set; }

        public PrizeCollection()
        {
            Prizes = new LinkedList<Prize>();
            TotalPrizeCount = 0;
        }

        public void UpdateList(double prizeAmount, int cardsLeft)
        {
            TotalPrizeCount += (int)prizeAmount;
            if (prizeAmount > cardsLeft)
            {
                int amountPerCard = Convert.ToInt32(prizeAmount / cardsLeft);
                double remainder = prizeAmount % cardsLeft;

                var prize = Prizes.First;

                for (int i = 0; i < cardsLeft; i++)
                {
                    var totalPrize = amountPerCard;
                    if (remainder > 0)
                    {
                        totalPrize++;
                        remainder--;
                    }
                    if (prize == null)
                    {
                        prize = new LinkedListNode<Prize>(new Prize(totalPrize));
                        Prizes.AddLast(prize);
                    }
                    else
                    {
                        prize.Value.UpdateValue(totalPrize);
                    }

                    prize = prize.Next;
                }
            }
            else
            {
                var prize = Prizes.First;

                for (int i = 0; i < prizeAmount; i++)
                {
                    if (prize == null)
                    {
                        prize = new LinkedListNode<Prize>(new Prize(1));
                        Prizes.AddLast(prize);
                    }
                    else
                    {
                        prize.Value.UpdateValue(1);
                    }

                    prize = prize.Next;
                }
            }
        }

        public Prize GetNextPrize()
        {
            var prizeNode = Prizes.First;
            if (prizeNode == null)
                return null;

            var prize = prizeNode.Value;
            Prizes.RemoveFirst();
            return prize;
        }
    }

    public class Number
    {
        public string Value { get; set; }

        public Number(string value)
        {
            Value = value.Trim();
        }

        public override bool Equals(object? number)
        {
            var numberValue = number as Number;
            return numberValue.Value == this.Value;
        }

        public override int GetHashCode()
        {
            int result = Value.GetHashCode();
            return result;
        }
    }

    public class Card
    {
        public bool IsOriginal { get; set; }
        public HashSet<Number> PrizedNumbers { get; set; }
        public List<Number> PlayedNumbers { get; set; }

        public Card(bool isOriginal, HashSet<Number> prizedNumbers, List<Number> playedNumbers)
        {
            IsOriginal = isOriginal;
            this.PrizedNumbers = prizedNumbers;
            this.PlayedNumbers = playedNumbers;
        }

        public double GetPoints()
        {
            int matches = 0;
            double sum = 0;
            foreach (var number in PlayedNumbers)
            {
                if (PrizedNumbers.Contains(number))
                {
                    matches++;
                }
            }
            if (matches != 0)
            {
                sum += matches;
            }

            return sum;
        }
    }

    public class Day4Problem2 : IProblem
    {
        private List<string> input;
        private LinkedList<Card> cards;
        private PrizeCollection prizeCollection;

        public Day4Problem2()
        {
            input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day4Problem1Input.txt")).ToList();
            cards = new LinkedList<Card>();
            prizeCollection = new PrizeCollection();
            ParseInput(input);
        }

        private void ParseInput(List<string> input)
        {
            foreach (string s in input)
            {
                var stringWithoutCardBit = Regex.Replace(s, @"[Card]+([ ])*([0-9])*[:][ ]", "");
                var splitList = new List<string>(stringWithoutCardBit.Split("|", StringSplitOptions.RemoveEmptyEntries));

                var prizedNumbersSplit = new List<string>(splitList[0].Split(" ", StringSplitOptions.RemoveEmptyEntries));
                var playedNumbersSplit = new List<string>(splitList[1].Split(" ", StringSplitOptions.RemoveEmptyEntries));

                var prizeSet = new HashSet<Number>();
                var playedList = new List<Number>();

                foreach (var number in prizedNumbersSplit)
                {
                    prizeSet.Add(new Number(number));
                }

                foreach (var number in playedNumbersSplit)
                {
                    playedList.Add(new Number(number));
                }

                var card = new Card(true, prizeSet, playedList);
                cards.AddLast(card);
            }
        }

        private void DuplicateCard(LinkedListNode<Card> cardNode, Prize prize)
        {
            if (prize != null)
            {
                var times = prize.Value;
                for (int i = 0; i < times; i++)
                {
                    var newCard = new Card(false, cardNode.Value.PrizedNumbers, cardNode.Value.PlayedNumbers);
                    cards.AddAfter(cardNode, newCard);
                }
            }
        }

        private double GetSum()
        {
            double sum = 0;

            var cardNode = cards.First;
            var remainingOriginalCards = cards.Count;
            while (cardNode != null)
            {
                var card = cardNode.Value;
                if (card.IsOriginal)
                {
                    remainingOriginalCards--;
                    DuplicateCard(cardNode, prizeCollection.GetNextPrize());
                }
                var points = card.GetPoints();
                prizeCollection.UpdateList(points, remainingOriginalCards);

                sum += points;
                cardNode = cardNode.Next;
            }

            return cards.Count;
        }

        public string SolveProblem()
        {
            var result = GetSum();
            return result.ToString();
        }
    }
}
