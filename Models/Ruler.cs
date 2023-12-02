namespace XMedicus.Models
{
    public class Ruler
    {
        public int Id { get; }
        public string Name { get; }
        public string FirstName
        {
            get => Name.Split(" ").First();
        }
        public Rule Rule { get; }
        public string? House { get; }

        public Ruler(int id, string name, Rule rule, string? house)
        {
            Id = id;
            Name = name;
            Rule = rule;
            House = house;
        }
    }

    public class Rule
    {
        private bool _toPresent { get; }
        public int? Beginning { get; }
        public int End { get; }
        public string Period
        {
            get => $"{(Beginning.HasValue ? Beginning + " - " : (!_toPresent ? "-" : ""))}{End}{(_toPresent ? $" - I dag" : "")}";
        }
        public int Length
        {
            get => (int) (Beginning.HasValue ? End - Beginning : (_toPresent ? DateTime.Now.Year - End : 0)); 
        }

        public Rule(int? beginning, int end)
        {
            if (beginning > end)
                throw new ArgumentOutOfRangeException($"The beginning {beginning} can't be after the end {end}");

            Beginning = beginning;
            End = end;
        }

        public Rule(int year, bool toPresent)
        {
            End = year;
            _toPresent = toPresent;
        }
    }
}