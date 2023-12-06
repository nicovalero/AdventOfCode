using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day5Problem1
{
    public class Element
    {
        public double ID;

        public Element(double ID)
        {
            this.ID = ID;
        }
    }

    public class RangeMapCollection
    {
        public List<RangeMap> RangeMaps;

        public RangeMapCollection()
        {
            RangeMaps = new List<RangeMap>();
        }

        public Element GetDestination(Element element)
        {
            foreach(RangeMap map in RangeMaps)
            {
                if(map.SourcePoint <= element.ID && map.SourcePoint + map.Range >= element.ID)
                {
                    var id = map.DestinationPoint + (element.ID - map.SourcePoint);
                    return new Element(id);
                }
            }
            return new Element(element.ID);
        }
    }

    public class RangeMap
    {
        public double SourcePoint { get; set; }
        public double DestinationPoint { get; set; }
        public double Range { get; set; }

        public RangeMap(string startingpoint, string endPoint, string range)
        {
            SourcePoint = Convert.ToDouble(startingpoint);
            DestinationPoint = Convert.ToDouble(endPoint);
            Range = Convert.ToDouble(range);
        }

        public override bool Equals(object? obj)
        {
            var instance = obj as RangeMap;
            if (instance.SourcePoint == this.SourcePoint &&
                instance.Range == this.Range &&
                instance.DestinationPoint == this.DestinationPoint)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsElement(Element element)
        {
            if (SourcePoint <= element.ID && DestinationPoint >= element.ID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class PathCombination
    {
        public List<Element> Elements { get; set; }

        public PathCombination()
        {
            Elements = new List<Element>();
        }

        public override bool Equals(object? obj)
        {
            var instance = obj as PathCombination;

            if(instance.Elements.Count == this.Elements.Count)
            {
                for(int i = 0; i < this.Elements.Count; i++)
                {
                    if (this.Elements[i].ID != instance.Elements[i].ID)
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    public class Day5Problem1 : IProblem
    {
        private List<string> input;
        private PathCombination pathCombination;

        private List<Element> seeds;
        private RangeMapCollection seedToSoil;
        private RangeMapCollection soilToFertilizer;
        private RangeMapCollection fertilizertoWater;
        private RangeMapCollection waterToLight;
        private RangeMapCollection lightToTemperature;
        private RangeMapCollection temperatureToHumidity;
        private RangeMapCollection humidityToLocation;

        private Dictionary<PathCombination, Element> memory;

        public string ID()
        {
            return "Day 5 Problem 1";
        }
        public Day5Problem1()
        {
            input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day5Problem1Input.txt")).ToList();
            memory = new Dictionary<PathCombination, Element>();
            ParseInput(input);
        }

        private void ParseInput(List<string> input)
        {
            seeds = new List<Element>();
            pathCombination = new PathCombination();
            seedToSoil = new RangeMapCollection();
            soilToFertilizer = new RangeMapCollection();
            fertilizertoWater = new RangeMapCollection();
            waterToLight = new RangeMapCollection();
            lightToTemperature = new RangeMapCollection();
            temperatureToHumidity = new RangeMapCollection();
            humidityToLocation = new RangeMapCollection();

            var stringWithoutSeedsBit = Regex.Replace(input[0], @"[seeds]+[:][ ]", "");
            var split = stringWithoutSeedsBit.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach(var number in split)
            {
                seeds.Add(new Element(Convert.ToDouble(number)));
            }

            var currentMap = "";
            input.RemoveAt(0);

            foreach (string s in input)
            {
                if(s.Contains("map"))
                {
                    currentMap = s;
                }
                else if(s == String.Empty)
                {

                }
                else
                {
                    var numbers = s.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                    var rangeMap = new RangeMap(numbers[1], numbers[0], numbers[2]);
                    switch (currentMap)
                    {
                        case "seed-to-soil map:":
                            seedToSoil.RangeMaps.Add(rangeMap);
                            break;
                        case "soil-to-fertilizer map:":
                            soilToFertilizer.RangeMaps.Add(rangeMap);
                            break;
                        case "fertilizer-to-water map:":
                            fertilizertoWater.RangeMaps.Add(rangeMap);
                            break;
                        case "water-to-light map:":
                            waterToLight.RangeMaps.Add(rangeMap);
                            break;
                        case "light-to-temperature map:":
                            lightToTemperature.RangeMaps.Add(rangeMap);
                            break;
                        case "temperature-to-humidity map:":
                            temperatureToHumidity.RangeMaps.Add(rangeMap);
                            break;
                        case "humidity-to-location map:":
                            humidityToLocation.RangeMaps.Add(rangeMap);
                            break;
                    }
                }
            }
        }

        private double GetLocation()
        {
            double minlocation = double.MaxValue;

            foreach(var element in seeds)
            {
                var currentElement = element;
                var path = new PathCombination();
                var queue = GetQueue();

                while(queue.Count > 0)
                {
                    var mapCollection = queue.Dequeue();
                    currentElement = mapCollection.GetDestination(currentElement);
                }
                if(currentElement.ID < minlocation)
                {
                    minlocation = currentElement.ID;
                }
            }

            return minlocation;
        }

        private Queue<RangeMapCollection> GetQueue()
        {
            var queue = new Queue<RangeMapCollection>();
            queue.Enqueue(seedToSoil);
            queue.Enqueue(soilToFertilizer);
            queue.Enqueue(fertilizertoWater);
            queue.Enqueue(waterToLight);
            queue.Enqueue(lightToTemperature);
            queue.Enqueue(temperatureToHumidity);
            queue.Enqueue(humidityToLocation);

            return queue;
        }

        public string SolveProblem()
        {
            var result = GetLocation();
            return result.ToString();
        }
    }
}
