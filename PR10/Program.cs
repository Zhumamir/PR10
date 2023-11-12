using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR10
{
    public class Program
    {
        static void Main(string[] args)
        {
            House house = new House
            {
                Basement = new Basement(),
                Walls = new Walls[4],
                Door = new Door(),
                Windows = new Window[4],
                Roof = new Roof()
            };

            Team team = new Team();
            team.Workers.Add(new Worker { Name = "Worker 1" });
            team.Workers.Add(new Worker { Name = "Worker 2" });
            team.Workers.Add(new Worker { Name = "Worker 3" });
            team.Workers.Add(new Worker { Name = "Worker 4" });
            team.Workers.Add(new TeamLeader { Name = "Team Leader" });

            team.BuildHouse(house);

            Console.WriteLine("Construction completed. House details:");
            house.ShowHouse();
        }
    }
    public class House
    {
        public Basement Basement { get; set; }
        public Walls[] Walls { get; set; }
        public Door Door { get; set; }
        public Window[] Windows { get; set; }
        public Roof Roof { get; set; }

        public void ShowHouse()
        {
            Console.WriteLine("House:");
            Console.WriteLine($"Basement: {(Basement.IsBuilt ? "Built" : "Not built")}");
            Console.WriteLine($"Walls: {Walls.Count(w => w.IsBuilt)} out of 4 built");
            Console.WriteLine($"Door: {(Door.IsBuilt ? "Built" : "Not built")}");
            Console.WriteLine($"Windows: {Windows.Count(w => w.IsBuilt)} out of 4 built");
            Console.WriteLine($"Roof: {(Roof.IsBuilt ? "Built" : "Not built")}");
        }
    }

    public class Basement : IPart
    {
        public string Name => "Basement";
        public bool IsBuilt { get; set; }
    }

    public class Walls : IPart
    {
        public string Name => "Wall";
        public bool IsBuilt { get; set; }
    }

    public class Door : IPart
    {
        public string Name => "Door";
        public bool IsBuilt { get; set; }
    }

    public class Window : IPart
    {
        public string Name => "Window";
        public bool IsBuilt { get; set; }
    }

    public class Roof : IPart
    {
        public string Name => "Roof";
        public bool IsBuilt { get; set; }
    }

    public class Team
    {
        public List<IWorker> Workers { get; set; } = new List<IWorker>();

        public void BuildHouse(House house)
        {
            foreach (var worker in Workers)
            {
                foreach (var part in new IPart[] { house.Basement, house.Door, house.Roof })
                {
                    worker.Build(part);
                }

                foreach (var part in house.Walls.Cast<IPart>().Concat(house.Windows.Cast<IPart>()))
                {
                    worker.Build(part);
                }
            }
        }
    }

    public class Worker : IWorker
    {
        public string Name { get; set; }

        public void Build(IPart part)
        {
            if (!part.IsBuilt)
            {
                part.IsBuilt = true;
                Console.WriteLine($"{Name} built the {part.Name}.");
            }
        }
    }

    public class TeamLeader : IWorker
    {
        public string Name { get; set; }

        public void Build(IPart part)
        {
            Console.WriteLine($"{Name} reports: {part.Name} is {(part.IsBuilt ? "Built" : "Not built")}");
        }
    }

}
