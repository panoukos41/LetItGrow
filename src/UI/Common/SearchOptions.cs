namespace LetItGrow.UI.Common
{
    public class SearchOptions
    {
        public int Days { get; set; }

        public SearchOptions(int days = 3) => Days = days;

        public static SearchOptions ThreeDays => new(3);

        public static SearchOptions FiveDays => new(5);

        public static SearchOptions OneWeek => new(7);

        public static SearchOptions OneMonth => new(30);

#pragma warning disable CA2211 // Non-constant fields should not be visible

        public static SearchOptions[] All = new[]
#pragma warning restore CA2211 // Non-constant fields should not be visible
        {
            ThreeDays,
            FiveDays,
            OneWeek,
            OneMonth
        };

        public override string ToString()
        {
            return $"{Days} Days";
        }

        public static implicit operator SearchOptions(int _) => new(_);

        public static implicit operator int(SearchOptions _) => _.Days;
    }
}