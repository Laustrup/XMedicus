namespace XMedicus.Models
{
    /// <summary>
    /// Object of a <c>Ruler</c> that has ruled the Danish nation.
    /// Also contains useful attributes that are calculated of inputs.
    /// </summary>
    public class Ruler
    {
        /// <summary>
        /// An unique identification of the <c>Ruler</c>
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The full name and aka of the <c>Ruler</c>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Only gets the first name out of the full name
        /// </summary>
        public string FirstName
        {
            get => Name.Split(" ").First();
        }

        /// <summary>
        /// An object that is created from a period between the <c>Ruler</c> has ruled
        /// including a start and end year
        /// </summary>
        public Rule Rule { get; }

        /// <summary>
        /// A family name of which house that <c>Ruler</c> belongs to
        /// </summary>
        public string? House { get; }

        /// <summary>
        /// Constructs a <c>Ruler<c> with all it's fields
        /// </summary>
        public Ruler(int id, string name, Rule rule, string? house)
        {
            Id = id;
            Name = name;
            Rule = rule;
            House = house;
        }
    }

    /// <summary>
    /// A start and end period of a <c>Ruler</c> which he as reign
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// Indicates if the <c>Ruler</c> is still in command
        /// </summary>
        private bool _toPresent { get; }

        /// <summary>
        /// That beginning of the <c>Rule</c> period
        /// </summary>
        public int? Beginning { get; }
        
        /// <summary>
        /// That end of the <c>Rule</c> period
        /// </summary>
        public int End { get; }

        /// <summary>
        /// A string ment for view in display
        /// </summary>
        public string Period
        {
            get => $"{(Beginning.HasValue ? Beginning + " - " : (!_toPresent ? "-" : ""))}{End}{(_toPresent ? $" - I dag" : "")}";
        }

        /// <summary>
        /// A calculated amount in year format of the interval between start and end
        /// </summary>
        public int Length
        {
            get => (int) (Beginning.HasValue ? End - Beginning : (_toPresent ? DateTime.Now.Year - End : 0)); 
        }

        /// <summary>
        /// A constructor that creates creates a Rule from a start to an end
        /// </summary>
        /// <param name="beginning">The start of the <c>Rule<c></param>
        /// <param name="end">The end of the <c>Rule<c></param>
        /// <exception cref="ArgumentOutOfRangeException">In case beginning is before end, this <c>Rule<c> will not be constructed</exception>
        public Rule(int? beginning, int end)
        {
            if (beginning > end)
                throw new ArgumentOutOfRangeException($"The beginning {beginning} can't be after the end {end}");

            Beginning = beginning;
            End = end;
        }

        /// <summary>
        /// A constructor with only one year, indicating it might be a start or end rule
        ///<summary>
        /// <param name="year">The only mentioned year, indicationg either start or end</param>
        /// <param name="toPresent">True if it is still a <c>Rule<c>, false if it is first</param>
        public Rule(int year, bool toPresent)
        {
            End = year;
            _toPresent = toPresent;
        }
    }
}